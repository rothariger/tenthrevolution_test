using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetRentals;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetRentals
{
    /// <summary>
    /// MediatR handler for the GetRentals request.
    /// </summary>
    public class GetRentalsHandler : IRequestHandler<GetRentalsRequest, Unit>
    {
        private readonly IUseCase<GetRentalsInput> _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRentalsHandler"/> class.
        /// </summary>
        /// <param name="useCase">The GetRentals use case.</param>
        public GetRentalsHandler(IUseCase<GetRentalsInput> useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(GetRentalsRequest request, CancellationToken cancellationToken)
        {
            System.ArgumentNullException.ThrowIfNull(request);
            await _useCase.Execute(new GetRentalsInput(request.ActiveOnly));
            return Unit.Value;
        }
    }
}
