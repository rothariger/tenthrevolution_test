using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetRentals
{
    /// <summary>
    /// MediatR request for retrieving the rental list.
    /// </summary>
    public class GetRentalsRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to return only currently active rentals.
        /// </summary>
        public bool ActiveOnly { get; set; }
    }
}
