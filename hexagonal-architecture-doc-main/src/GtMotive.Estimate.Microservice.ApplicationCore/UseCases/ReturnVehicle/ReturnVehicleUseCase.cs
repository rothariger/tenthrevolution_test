using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Use case for returning a rented vehicle.
    /// </summary>
    public class ReturnVehicleUseCase : IUseCase<ReturnVehicleInput>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IOutputPortStandard<ReturnVehicleOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        /// <param name="rentalRepository">The rental repository.</param>
        /// <param name="outputPort">The output port.</param>
        public ReturnVehicleUseCase(
            IRentalRepository rentalRepository,
            IOutputPortStandard<ReturnVehicleOutput> outputPort)
        {
            _rentalRepository = rentalRepository;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var rental = await _rentalRepository.GetByIdAsync(input.RentalId);
            if (rental is null)
            {
                throw new DomainException($"Rental '{input.RentalId}' not found.");
            }

            if (rental.ReturnedAt is not null)
            {
                throw new DomainException($"Rental '{input.RentalId}' is already closed.");
            }

            rental.MarkAsReturned(DateTime.UtcNow);
            await _rentalRepository.UpdateAsync(rental);

            _outputPort.StandardHandle(new ReturnVehicleOutput(rental.Id));
        }
    }
}
