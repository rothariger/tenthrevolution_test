using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// MediatR request for renting a vehicle.
    /// </summary>
    public class RentVehicleRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        required public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        required public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the planned start date of the rental.
        /// </summary>
        required public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the planned end date of the rental.
        /// </summary>
        required public DateTime EndDate { get; set; }
    }
}
