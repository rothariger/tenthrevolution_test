using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// MediatR handler for the CreateVehicle request.
    /// </summary>
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleRequest, Unit>
    {
        private readonly IUseCase<CreateVehicleInput> _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleHandler"/> class.
        /// </summary>
        /// <param name="useCase">The CreateVehicle use case.</param>
        public CreateVehicleHandler(IUseCase<CreateVehicleInput> useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            System.ArgumentNullException.ThrowIfNull(request);
            var input = new CreateVehicleInput(request.Make, request.Model, request.ManufactureYear);
            await _useCase.Execute(input);
            return Unit.Value;
        }
    }
}
