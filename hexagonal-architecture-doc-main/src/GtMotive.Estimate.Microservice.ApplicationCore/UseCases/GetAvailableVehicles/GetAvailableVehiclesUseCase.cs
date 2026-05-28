using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Use case for retrieving all available vehicles.
    /// </summary>
    public class GetAvailableVehiclesUseCase : IUseCase<GetAvailableVehiclesInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IOutputPortStandard<GetAvailableVehiclesOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="rentalRepository">The rental repository.</param>
        /// <param name="outputPort">The output port.</param>
        public GetAvailableVehiclesUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IOutputPortStandard<GetAvailableVehiclesOutput> outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(GetAvailableVehiclesInput input)
        {
            var activeRentals = await _rentalRepository.GetAllAsync(activeOnly: true);
            var rentedVehicleIds = new HashSet<System.Guid>(activeRentals.Select(r => r.VehicleId));

            var vehicles = await _vehicleRepository.GetAllAsync();
            var items = vehicles
                .Where(v => !rentedVehicleIds.Contains(v.Id))
                .Select(v => new VehicleItem(v.Id, v.Make, v.Model, v.ManufactureYear));

            _outputPort.StandardHandle(new GetAvailableVehiclesOutput(items));
        }
    }
}
