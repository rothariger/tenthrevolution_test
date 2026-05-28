using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle to a customer.
    /// </summary>
    public class RentVehicleUseCase : IUseCase<RentVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IBusFactory _busFactory;
        private readonly IOutputPortStandard<RentVehicleOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="rentalRepository">The rental repository.</param>
        /// <param name="busFactory">The bus factory for publishing events.</param>
        /// <param name="outputPort">The output port.</param>
        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IBusFactory busFactory,
            IOutputPortStandard<RentVehicleOutput> outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _busFactory = busFactory;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.StartDate.Date <= DateTime.UtcNow.Date)
            {
                throw new DomainException("Start date must be in the future.");
            }

            if (input.EndDate <= input.StartDate)
            {
                throw new DomainException("End date must be after the start date.");
            }

            var customerHasOverlap = await _rentalRepository.HasOverlappingRentalForCustomerAsync(
                input.CustomerId, input.StartDate, input.EndDate);
            if (customerHasOverlap)
            {
                throw new DomainException($"Customer '{input.CustomerId}' already has a rental overlapping the requested period.");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
            if (vehicle is null)
            {
                throw new DomainException($"Vehicle '{input.VehicleId}' not found.");
            }

            var vehicleHasOverlap = await _rentalRepository.HasOverlappingRentalForVehicleAsync(
                input.VehicleId, input.StartDate, input.EndDate);
            if (vehicleHasOverlap)
            {
                throw new DomainException($"Vehicle '{input.VehicleId}' is not available for the requested period.");
            }

            var rental = new Rental(Guid.NewGuid(), vehicle.Id, input.CustomerId, input.StartDate, input.EndDate, returnedAt: null);
            await _rentalRepository.AddAsync(rental);

            var bus = _busFactory.GetClient(typeof(RentVehicleOutput));
            await bus.Send(new RentVehicleOutput(rental.Id));

            _outputPort.StandardHandle(new RentVehicleOutput(rental.Id));
        }
    }
}
