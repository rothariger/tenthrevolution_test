#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Extensions;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IRentalRepository"/>.
    /// </summary>
    public class MongoRentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<RentalDocument> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRentalRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        public MongoRentalRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _collection = mongoService.GetDatabase().GetCollection<RentalDocument>("rentals");
        }

        /// <inheritdoc/>
        public async Task AddAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            await _collection.InsertOneAsync(ToDocument(rental));
        }

        /// <inheritdoc/>
        public async Task<Rental?> GetByIdAsync(Guid id)
        {
            var cursor = await _collection.FindAsync(d => d.Id == id);
            var document = await cursor.FirstOrDefaultAsync();
            return document?.ToRental();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            await _collection.ReplaceOneAsync(d => d.Id == rental.Id, ToDocument(rental));
        }

        /// <inheritdoc/>
        public async Task<bool> HasOverlappingRentalForVehicleAsync(Guid vehicleId, DateTime startDate, DateTime endDate)
        {
            var cursor = await _collection.FindAsync(d =>
                d.VehicleId == vehicleId &&
                d.ReturnedAt == null &&
                d.StartDate < endDate &&
                d.EndDate > startDate);
            return await cursor.AnyAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> HasOverlappingRentalForCustomerAsync(string customerId, DateTime startDate, DateTime endDate)
        {
            var cursor = await _collection.FindAsync(d =>
                d.CustomerId == customerId &&
                d.ReturnedAt == null &&
                d.StartDate < endDate &&
                d.EndDate > startDate);
            return await cursor.AnyAsync();
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Rental>> GetAllAsync(bool activeOnly)
        {
            List<RentalDocument> documents;

            if (activeOnly)
            {
                var now = DateTime.UtcNow;
                var cursor = await _collection.FindAsync(d =>
                    d.StartDate <= now &&
                    d.EndDate >= now &&
                    d.ReturnedAt == null);
                documents = await cursor.ToListAsync();
            }
            else
            {
                var cursor = await _collection.FindAsync(_ => true);
                documents = await cursor.ToListAsync();
            }

            return documents.ConvertAll(d => d.ToRental());
        }

        private static RentalDocument ToDocument(Rental rental)
        {
            return new RentalDocument
            {
                Id = rental.Id,
                VehicleId = rental.VehicleId,
                CustomerId = rental.CustomerId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                ReturnedAt = rental.ReturnedAt
            };
        }
    }
}
