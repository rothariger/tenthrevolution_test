using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle in the rental fleet.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="make">The vehicle manufacturer.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureYear">The year the vehicle was manufactured.</param>
        public Vehicle(Guid id, string make, string model, int manufactureYear)
        {
            Id = id;
            Make = make;
            Model = model;
            ManufactureYear = manufactureYear;
        }

        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the manufacturer of the vehicle.
        /// </summary>
        public string Make { get; private set; }

        /// <summary>
        /// Gets the model of the vehicle.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Gets the year the vehicle was manufactured.
        /// </summary>
        public int ManufactureYear { get; private set; }
    }
}
