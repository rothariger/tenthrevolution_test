using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// MediatR request for creating a new vehicle.
    /// </summary>
    public class CreateVehicleRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the vehicle manufacturer.
        /// </summary>
        required public string Make { get; set; }

        /// <summary>
        /// Gets or sets the vehicle model.
        /// </summary>
        required public string Model { get; set; }

        /// <summary>
        /// Gets or sets the year the vehicle was manufactured.
        /// </summary>
        required public int ManufactureYear { get; set; }
    }
}
