using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetRentals
{
    /// <summary>
    /// Output for the GetRentals use case.
    /// </summary>
    public class GetRentalsOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRentalsOutput"/> class.
        /// </summary>
        /// <param name="rentals">The list of rentals.</param>
        public GetRentalsOutput(IEnumerable<RentalItem> rentals)
        {
            Rentals = rentals;
        }

        /// <summary>
        /// Gets the list of rentals.
        /// </summary>
        public IEnumerable<RentalItem> Rentals { get; }
    }
}
