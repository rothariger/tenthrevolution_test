using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// MediatR handler for the ReturnVehicle request.
    /// </summary>
    public class ReturnVehicleHandler : IRequestHandler<ReturnVehicleRequest, Unit>
    {
        private readonly IUseCase<ReturnVehicleInput> _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleHandler"/> class.
        /// </summary>
        /// <param name="useCase">The ReturnVehicle use case.</param>
        public ReturnVehicleHandler(IUseCase<ReturnVehicleInput> useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            System.ArgumentNullException.ThrowIfNull(request);
            await _useCase.Execute(new ReturnVehicleInput(request.RentalId));
            return Unit.Value;
        }
    }
}
