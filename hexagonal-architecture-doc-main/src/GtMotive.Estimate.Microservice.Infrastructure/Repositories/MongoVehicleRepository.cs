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
    /// MongoDB implementation of <see cref="IVehicleRepository"/>.
    /// </summary>
    public class MongoVehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<VehicleDocument> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoVehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        public MongoVehicleRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _collection = mongoService.GetDatabase().GetCollection<VehicleDocument>("vehicles");
        }

        /// <inheritdoc/>
        public async Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            var document = ToDocument(vehicle);
            await _collection.InsertOneAsync(document);
        }

        /// <inheritdoc/>
        public async Task<Vehicle?> GetByIdAsync(Guid id)
        {
            var cursor = await _collection.FindAsync(d => d.Id == id);
            var document = await cursor.FirstOrDefaultAsync();
            return document?.ToVehicle();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            var document = ToDocument(vehicle);
            await _collection.ReplaceOneAsync(d => d.Id == vehicle.Id, document);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            var cursor = await _collection.FindAsync(_ => true);
            var documents = await cursor.ToListAsync();
            var vehicles = new List<Vehicle>(documents.Count);
            foreach (var doc in documents)
            {
                vehicles.Add(doc.ToVehicle());
            }

            return vehicles;
        }

        private static VehicleDocument ToDocument(Vehicle vehicle)
        {
            return new VehicleDocument
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                ManufactureYear = vehicle.ManufactureYear
            };
        }
    }
}
