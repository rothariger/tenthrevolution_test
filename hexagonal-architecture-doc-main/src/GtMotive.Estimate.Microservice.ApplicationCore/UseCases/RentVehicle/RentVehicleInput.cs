using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input for the RentVehicle use case.
    /// </summary>
    public class RentVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle to rent.</param>
        /// <param name="customerId">The customer renting the vehicle.</param>
        /// <param name="startDate">The planned start date of the rental.</param>
        /// <param name="endDate">The planned end date of the rental.</param>
        public RentVehicleInput(Guid vehicleId, string customerId, DateTime startDate, DateTime endDate)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public string CustomerId { get; }

        /// <summary>
        /// Gets the planned start date of the rental.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the planned end date of the rental.
        /// </summary>
        public DateTime EndDate { get; }
    }
}
