#nullable enable
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    [Collection(TestCollections.TestServer)]
    public sealed class VehiclesControllerTests(GenericInfrastructureTestServerFixture fixture)
    {
        [Fact]
        public async Task GetAvailableVehicles_ReturnsOk()
        {
            var client = fixture.Server.CreateClient();

            var response = await client.GetAsync(new Uri("/api/vehicles/available", UriKind.Relative));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
