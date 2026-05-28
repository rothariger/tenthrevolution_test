using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// MediatR handler for the GetAvailableVehicles request.
    /// </summary>
    public class GetAvailableVehiclesHandler : IRequestHandler<GetAvailableVehiclesRequest, Unit>
    {
        private readonly IUseCase<GetAvailableVehiclesInput> _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesHandler"/> class.
        /// </summary>
        /// <param name="useCase">The GetAvailableVehicles use case.</param>
        public GetAvailableVehiclesHandler(IUseCase<GetAvailableVehiclesInput> useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(GetAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new GetAvailableVehiclesInput());
            return Unit.Value;
        }
    }
}
