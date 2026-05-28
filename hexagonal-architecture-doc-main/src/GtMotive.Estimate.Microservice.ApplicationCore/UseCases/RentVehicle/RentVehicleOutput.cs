using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output for the RentVehicle use case.
    /// </summary>
    public class RentVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleOutput"/> class.
        /// </summary>
        /// <param name="rentalId">The identifier of the created rental.</param>
        public RentVehicleOutput(Guid rentalId)
        {
            RentalId = rentalId;
        }

        /// <summary>
        /// Gets the identifier of the created rental.
        /// </summary>
        public Guid RentalId { get; }
    }
}
