using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents an active or completed vehicle rental.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="vehicleId">The rented vehicle identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="startDate">The planned start date of the rental.</param>
        /// <param name="endDate">The planned end date of the rental.</param>
        /// <param name="returnedAt">The actual return date and time, or null if not yet returned.</param>
        public Rental(Guid id, Guid vehicleId, string customerId, DateTime startDate, DateTime endDate, DateTime? returnedAt)
        {
            Id = id;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            ReturnedAt = returnedAt;
        }

        /// <summary>
        /// Gets the unique identifier of the rental.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the identifier of the rented vehicle.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Gets the identifier of the customer who rented the vehicle.
        /// </summary>
        public string CustomerId { get; private set; }

        /// <summary>
        /// Gets the planned start date of the rental.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the planned end date of the rental.
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Gets the actual return date and time, or null if not yet returned.
        /// </summary>
        public DateTime? ReturnedAt { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this rental is currently active
        /// (today falls within the rental period and the vehicle has not been returned).
        /// </summary>
        public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow && ReturnedAt is null;

        /// <summary>
        /// Marks the rental as returned at the given date and time.
        /// </summary>
        /// <param name="returnedAt">The return date and time.</param>
        public void MarkAsReturned(DateTime returnedAt)
        {
            ReturnedAt = returnedAt;
        }
    }
}
