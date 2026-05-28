using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetRentals
{
    /// <summary>
    /// Use case for retrieving the list of rentals.
    /// </summary>
    public class GetRentalsUseCase : IUseCase<GetRentalsInput>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IOutputPortStandard<GetRentalsOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRentalsUseCase"/> class.
        /// </summary>
        /// <param name="rentalRepository">The rental repository.</param>
        /// <param name="outputPort">The output port.</param>
        public GetRentalsUseCase(
            IRentalRepository rentalRepository,
            IOutputPortStandard<GetRentalsOutput> outputPort)
        {
            _rentalRepository = rentalRepository;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(GetRentalsInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var rentals = await _rentalRepository.GetAllAsync(input.ActiveOnly);

            var items = rentals.Select(r => new RentalItem(
                r.Id,
                r.VehicleId,
                r.CustomerId,
                r.StartDate,
                r.EndDate,
                r.ReturnedAt,
                r.IsActive));

            _outputPort.StandardHandle(new GetRentalsOutput(items));
        }
    }
}
