using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for returning rented vehicles.
    /// </summary>
    [ApiController]
    [Route("api/rentals/{id}/return")]
    public class RentalReturnController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ReturnVehiclePresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalReturnController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="presenter">The presenter.</param>
        public RentalReturnController(IMediator mediator, ReturnVehiclePresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Return([FromRoute] Guid id)
        {
            await _mediator.Send(new ReturnVehicleRequest { RentalId = id });
            return _presenter.ActionResult;
        }
    }
}
