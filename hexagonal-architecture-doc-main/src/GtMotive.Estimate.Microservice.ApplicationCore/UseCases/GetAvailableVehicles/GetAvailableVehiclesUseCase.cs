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
        private readonly IOutputPortStandard<GetAvailableVehiclesOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="outputPort">The output port.</param>
        public GetAvailableVehiclesUseCase(IVehicleRepository vehicleRepository, IOutputPortStandard<GetAvailableVehiclesOutput> outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(GetAvailableVehiclesInput input)
        {
            var vehicles = await _vehicleRepository.GetAllAvailableAsync();
            var items = vehicles.Select(v => new VehicleItem(v.Id, v.Make, v.Model, v.ManufactureYear));
            _outputPort.StandardHandle(new GetAvailableVehiclesOutput(items));
        }
    }
}
