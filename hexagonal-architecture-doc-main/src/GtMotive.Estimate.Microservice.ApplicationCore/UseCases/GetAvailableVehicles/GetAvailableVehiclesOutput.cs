using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Output for the GetAvailableVehicles use case.
    /// </summary>
    public class GetAvailableVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesOutput"/> class.
        /// </summary>
        /// <param name="vehicles">The list of available vehicles.</param>
        public GetAvailableVehiclesOutput(IEnumerable<VehicleItem> vehicles)
        {
            Vehicles = vehicles;
        }

        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        public IEnumerable<VehicleItem> Vehicles { get; }
    }
}
