using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Extensions
{
    /// <summary>
    /// Extension methods for mapping between <see cref="RentalDocument"/> and <see cref="Rental"/>.
    /// </summary>
    public static class RentalDocumentExtensions
    {
        /// <summary>
        /// Converts a <see cref="RentalDocument"/> to a <see cref="Rental"/> domain entity.
        /// </summary>
        /// <param name="doc">The MongoDB document to convert.</param>
        /// <returns>A <see cref="Rental"/> reconstituted from persisted data.</returns>
        public static Rental ToRental(this RentalDocument doc)
        {
            ArgumentNullException.ThrowIfNull(doc);
            return new Rental(doc.Id, doc.VehicleId, doc.CustomerId, doc.StartDate, doc.EndDate, doc.ReturnedAt);
        }
    }
}
