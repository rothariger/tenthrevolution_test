#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Rental persistence.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Gets a rental by its identifier.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <returns>The rental, or null if not found.</returns>
        Task<Rental?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new rental to the repository.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Rental rental);

        /// <summary>
        /// Updates an existing rental in the repository.
        /// </summary>
        /// <param name="rental">The rental to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Rental rental);

        /// <summary>
        /// Returns true if the vehicle has any non-returned rental whose date range overlaps
        /// with [startDate, endDate].
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="startDate">The requested start date.</param>
        /// <param name="endDate">The requested end date.</param>
        /// <returns>True if an overlap exists; otherwise false.</returns>
        Task<bool> HasOverlappingRentalForVehicleAsync(Guid vehicleId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Returns true if the customer has any non-returned rental whose date range overlaps
        /// with [startDate, endDate].
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="startDate">The requested start date.</param>
        /// <param name="endDate">The requested end date.</param>
        /// <returns>True if an overlap exists; otherwise false.</returns>
        Task<bool> HasOverlappingRentalForCustomerAsync(string customerId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Returns all rentals, optionally filtered to only those currently active.
        /// </summary>
        /// <param name="activeOnly">When true, returns only rentals where today falls within the rental period and the vehicle has not been returned.</param>
        /// <returns>A read-only list of rentals.</returns>
        Task<IReadOnlyList<Rental>> GetAllAsync(bool activeOnly);
    }
}
