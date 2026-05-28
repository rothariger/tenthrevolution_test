using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetRentals
{
    /// <summary>
    /// Represents a single rental in the list response.
    /// </summary>
    public class RentalItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalItem"/> class.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="startDate">The planned start date.</param>
        /// <param name="endDate">The planned end date.</param>
        /// <param name="returnedAt">The actual return date, or null if not yet returned.</param>
        /// <param name="isActive">Whether the rental is currently active.</param>
        public RentalItem(Guid id, Guid vehicleId, string customerId, DateTime startDate, DateTime endDate, DateTime? returnedAt, bool isActive)
        {
            Id = id;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            ReturnedAt = returnedAt;
            IsActive = isActive;
        }

        /// <summary>Gets the rental identifier.</summary>
        public Guid Id { get; }

        /// <summary>Gets the vehicle identifier.</summary>
        public Guid VehicleId { get; }

        /// <summary>Gets the customer identifier.</summary>
        public string CustomerId { get; }

        /// <summary>Gets the planned start date.</summary>
        public DateTime StartDate { get; }

        /// <summary>Gets the planned end date.</summary>
        public DateTime EndDate { get; }

        /// <summary>Gets the actual return date, or null if not yet returned.</summary>
        public DateTime? ReturnedAt { get; }

        /// <summary>Gets a value indicating whether the rental is currently active.</summary>
        public bool IsActive { get; }
    }
}
