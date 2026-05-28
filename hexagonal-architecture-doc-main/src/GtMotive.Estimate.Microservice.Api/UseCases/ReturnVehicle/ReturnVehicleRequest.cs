using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// MediatR request for returning a rented vehicle.
    /// </summary>
    public class ReturnVehicleRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the rental identifier.
        /// </summary>
        required public Guid RentalId { get; set; }
    }
}
