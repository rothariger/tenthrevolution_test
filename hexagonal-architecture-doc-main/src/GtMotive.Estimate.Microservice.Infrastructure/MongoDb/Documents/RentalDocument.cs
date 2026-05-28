using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents
{
    /// <summary>
    /// MongoDB document representation of a Rental.
    /// </summary>
    public class RentalDocument
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the rented vehicle identifier.
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the planned start date of the rental.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the planned end date of the rental.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the actual return date and time, or null if not yet returned.
        /// </summary>
        public DateTime? ReturnedAt { get; set; }
    }
}
