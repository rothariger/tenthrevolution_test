namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input for the CreateVehicle use case.
    /// </summary>
    public class CreateVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleInput"/> class.
        /// </summary>
        /// <param name="make">The vehicle manufacturer.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureYear">The year the vehicle was manufactured.</param>
        public CreateVehicleInput(string make, string model, int manufactureYear)
        {
            Make = make;
            Model = model;
            ManufactureYear = manufactureYear;
        }

        /// <summary>
        /// Gets the vehicle manufacturer.
        /// </summary>
        public string Make { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the manufacture year.
        /// </summary>
        public int ManufactureYear { get; }
    }
}
