namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetRentals
{
    /// <summary>
    /// Input for the GetRentals use case.
    /// </summary>
    public class GetRentalsInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRentalsInput"/> class.
        /// </summary>
        /// <param name="activeOnly">When true, returns only rentals currently in progress.</param>
        public GetRentalsInput(bool activeOnly)
        {
            ActiveOnly = activeOnly;
        }

        /// <summary>
        /// Gets a value indicating whether to return only currently active rentals.
        /// </summary>
        public bool ActiveOnly { get; }
    }
}
