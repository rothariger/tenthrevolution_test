using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Use case for creating a new vehicle in the rental fleet.
    /// </summary>
    public class CreateVehicleUseCase : IUseCase<CreateVehicleInput>
    {
        private const int MaxVehicleAgeInYears = 5;

        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOutputPortStandard<CreateVehicleOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="outputPort">The output port.</param>
        public CreateVehicleUseCase(IVehicleRepository vehicleRepository, IOutputPortStandard<CreateVehicleOutput> outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var minAllowedYear = DateTime.UtcNow.Year - MaxVehicleAgeInYears;
            if (input.ManufactureYear < minAllowedYear)
            {
                throw new DomainException(
                    $"Vehicle manufacture year {input.ManufactureYear} exceeds the maximum allowed age of {MaxVehicleAgeInYears} years.");
            }

            var vehicle = new Vehicle(Guid.NewGuid(), input.Make, input.Model, input.ManufactureYear, isAvailable: true);
            await _vehicleRepository.AddAsync(vehicle);
            _outputPort.StandardHandle(new CreateVehicleOutput(vehicle.Id));
        }
    }
}
