using System;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    [Collection(TestCollections.Functional)]
    public sealed class CreateVehicleFunctionalTests(CompositionRootTestFixture fixture)
    {
        [Fact]
        public async Task CreateVehicle_ShouldPersistVehicleInRepository()
        {
            var request = new CreateVehicleRequest
            {
                Make = "Toyota",
                Model = "Corolla",
                ManufactureYear = DateTime.UtcNow.Year - 1
            };

            await fixture.UsingHandlerForRequest<CreateVehicleRequest>(async handler =>
            {
                await handler.Handle(request, default);
            });

            await fixture.UsingRepository<IVehicleRepository>(async repo =>
            {
                var vehicles = await repo.GetAllAsync();
                vehicles.Should().NotBeEmpty();
            });
        }
    }
}
