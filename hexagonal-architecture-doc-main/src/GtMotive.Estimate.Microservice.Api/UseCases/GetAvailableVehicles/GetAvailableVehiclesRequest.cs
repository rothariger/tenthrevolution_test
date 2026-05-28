using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// MediatR request for retrieving available vehicles.
    /// </summary>
    public class GetAvailableVehiclesRequest : IRequest<Unit>
    {
    }
}
