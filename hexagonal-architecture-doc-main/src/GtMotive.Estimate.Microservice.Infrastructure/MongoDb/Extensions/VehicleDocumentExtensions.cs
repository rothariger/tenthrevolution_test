using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Extensions
{
    /// <summary>
    /// Extension methods for mapping between <see cref="VehicleDocument"/> and <see cref="Vehicle"/>.
    /// </summary>
    public static class VehicleDocumentExtensions
    {
        /// <summary>
        /// Converts a <see cref="VehicleDocument"/> to a <see cref="Vehicle"/> domain entity.
        /// </summary>
        /// <param name="doc">The MongoDB document to convert.</param>
        /// <returns>A <see cref="Vehicle"/> reconstituted from persisted data.</returns>
        public static Vehicle ToVehicle(this VehicleDocument doc)
        {
            ArgumentNullException.ThrowIfNull(doc);
            return new Vehicle(doc.Id, doc.Make, doc.Model, doc.ManufactureYear);
        }
    }
}
