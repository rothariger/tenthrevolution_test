using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.GetRentals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for listing rentals.
    /// </summary>
    [ApiController]
    [Route("api/rentals")]
    public class GetRentalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetRentalsPresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRentalsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="presenter">The presenter.</param>
        public GetRentalsController(IMediator mediator, GetRentalsPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Returns the list of rentals, optionally filtered to only those currently active.
        /// </summary>
        /// <param name="activeOnly">When true, returns only rentals where today falls within the rental period and the vehicle has not been returned.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetRentals([FromQuery] bool activeOnly = false)
        {
            await _mediator.Send(new GetRentalsRequest { ActiveOnly = activeOnly });
            return _presenter.ActionResult;
        }
    }
}
