using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Represents a single vehicle item in the available vehicles list.
    /// </summary>
    public class VehicleItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleItem"/> class.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <param name="make">The vehicle manufacturer.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureYear">The manufacture year.</param>
        public VehicleItem(Guid id, string make, string model, int manufactureYear)
        {
            Id = id;
            Make = make;
            Model = model;
            ManufactureYear = manufactureYear;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the vehicle manufacturer.
        /// </summary>
        public string Make { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the manufacture year.
        /// </summary>
        public int ManufactureYear { get; }
    }
}
