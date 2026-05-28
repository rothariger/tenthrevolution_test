using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// MediatR handler for the RentVehicle request.
    /// </summary>
    public class RentVehicleHandler : IRequestHandler<RentVehicleRequest, Unit>
    {
        private readonly IUseCase<RentVehicleInput> _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleHandler"/> class.
        /// </summary>
        /// <param name="useCase">The RentVehicle use case.</param>
        public RentVehicleHandler(IUseCase<RentVehicleInput> useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            System.ArgumentNullException.ThrowIfNull(request);
            await _useCase.Execute(new RentVehicleInput(request.VehicleId, request.CustomerId, request.StartDate, request.EndDate));
            return Unit.Value;
        }
    }
}
