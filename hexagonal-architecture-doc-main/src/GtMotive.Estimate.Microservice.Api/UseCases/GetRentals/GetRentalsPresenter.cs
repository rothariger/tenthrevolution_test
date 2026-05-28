using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetRentals;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetRentals
{
    /// <summary>
    /// Presenter for the GetRentals use case.
    /// </summary>
    public class GetRentalsPresenter : IOutputPortStandard<GetRentalsOutput>, IWebApiPresenter
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc/>
        public void StandardHandle(GetRentalsOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);
            ActionResult = new OkObjectResult(response.Rentals);
        }
    }
}
