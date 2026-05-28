using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for creating rentals.
    /// </summary>
    [ApiController]
    [Route("api/rentals")]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RentVehiclePresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="presenter">The presenter.</param>
        public RentalsController(IMediator mediator, RentVehiclePresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Rents a vehicle to a customer.
        /// </summary>
        /// <param name="request">The rent vehicle request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Rent([FromBody] RentVehicleRequest request)
        {
            await _mediator.Send(request);
            return _presenter.ActionResult;
        }
    }
}
