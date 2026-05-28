using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output for the ReturnVehicle use case.
    /// </summary>
    public class ReturnVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleOutput"/> class.
        /// </summary>
        /// <param name="rentalId">The identifier of the closed rental.</param>
        public ReturnVehicleOutput(Guid rentalId)
        {
            RentalId = rentalId;
        }

        /// <summary>
        /// Gets the identifier of the closed rental.
        /// </summary>
        public Guid RentalId { get; }
    }
}
