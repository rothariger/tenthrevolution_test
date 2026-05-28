using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for listing available vehicles.
    /// </summary>
    [ApiController]
    [Route("api/vehicles/available")]
    public class AvailableVehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetAvailableVehiclesPresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailableVehiclesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="presenter">The presenter.</param>
        public AvailableVehiclesController(IMediator mediator, GetAvailableVehiclesPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Returns all vehicles currently available for rental.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAvailable()
        {
            await _mediator.Send(new GetAvailableVehiclesRequest());
            return _presenter.ActionResult;
        }
    }
}
