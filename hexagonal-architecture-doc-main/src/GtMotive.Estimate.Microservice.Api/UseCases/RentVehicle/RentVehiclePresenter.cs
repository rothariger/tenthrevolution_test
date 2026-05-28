using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Presenter for the RentVehicle use case.
    /// </summary>
    public class RentVehiclePresenter : IOutputPortStandard<RentVehicleOutput>, IWebApiPresenter
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc/>
        public void StandardHandle(RentVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);
            ActionResult = new CreatedResult(string.Empty, new { response.RentalId });
        }
    }
}
