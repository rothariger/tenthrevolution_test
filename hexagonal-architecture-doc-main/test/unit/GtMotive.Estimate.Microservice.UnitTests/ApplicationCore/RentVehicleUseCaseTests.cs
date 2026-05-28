#nullable enable
using System;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    public sealed class RentVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo = new();
        private readonly Mock<IRentalRepository> _rentalRepo = new();
        private readonly Mock<IBusFactory> _busFactory = new();
        private readonly Mock<IBus> _bus = new();
        private readonly Mock<IOutputPortStandard<RentVehicleOutput>> _outputPort = new();

        [Fact]
        public async Task Execute_WhenStartDateIsToday_ThrowsDomainException()
        {
            var input = new RentVehicleInput(
                Guid.NewGuid(),
                "customer1",
                DateTime.UtcNow.Date,
                DateTime.UtcNow.Date.AddDays(3));

            var act = () => CreateSut().Execute(input);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Execute_WhenEndDateNotAfterStartDate_ThrowsDomainException()
        {
            var start = DateTime.UtcNow.Date.AddDays(1);
            var input = new RentVehicleInput(Guid.NewGuid(), "customer1", start, start);

            var act = () => CreateSut().Execute(input);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Execute_WhenCustomerHasOverlappingRental_ThrowsDomainException()
        {
            var start = DateTime.UtcNow.Date.AddDays(1);
            var end = start.AddDays(3);
            var input = new RentVehicleInput(Guid.NewGuid(), "customer1", start, end);

            _rentalRepo
                .Setup(r => r.HasOverlappingRentalForCustomerAsync("customer1", start, end))
                .ReturnsAsync(true);

            var act = () => CreateSut().Execute(input);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Execute_WhenVehicleNotFound_ThrowsDomainException()
        {
            var vehicleId = Guid.NewGuid();
            var start = DateTime.UtcNow.Date.AddDays(1);
            var end = start.AddDays(3);
            var input = new RentVehicleInput(vehicleId, "customer1", start, end);

            _rentalRepo
                .Setup(r => r.HasOverlappingRentalForCustomerAsync(
                    It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);
            _vehicleRepo
                .Setup(r => r.GetByIdAsync(vehicleId))
                .ReturnsAsync((Vehicle?)null);

            var act = () => CreateSut().Execute(input);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Execute_WhenVehicleHasOverlappingRental_ThrowsDomainException()
        {
            var vehicleId = Guid.NewGuid();
            var start = DateTime.UtcNow.Date.AddDays(1);
            var end = start.AddDays(3);
            var input = new RentVehicleInput(vehicleId, "customer1", start, end);

            _rentalRepo
                .Setup(r => r.HasOverlappingRentalForCustomerAsync(
                    It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);
            _vehicleRepo
                .Setup(r => r.GetByIdAsync(vehicleId))
                .ReturnsAsync(new Vehicle(vehicleId, "Toyota", "Corolla", 2022));
            _rentalRepo
                .Setup(r => r.HasOverlappingRentalForVehicleAsync(vehicleId, start, end))
                .ReturnsAsync(true);

            var act = () => CreateSut().Execute(input);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Execute_WhenAllValid_CreatesRentalAndCallsOutputPort()
        {
            var vehicleId = Guid.NewGuid();
            var start = DateTime.UtcNow.Date.AddDays(1);
            var end = start.AddDays(3);
            var input = new RentVehicleInput(vehicleId, "customer1", start, end);

            _rentalRepo
                .Setup(r => r.HasOverlappingRentalForCustomerAsync(
                    It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);
            _vehicleRepo
                .Setup(r => r.GetByIdAsync(vehicleId))
                .ReturnsAsync(new Vehicle(vehicleId, "Toyota", "Corolla", 2022));
            _rentalRepo
                .Setup(r => r.HasOverlappingRentalForVehicleAsync(vehicleId, start, end))
                .ReturnsAsync(false);
            _busFactory
                .Setup(f => f.GetClient(It.IsAny<Type>()))
                .Returns(_bus.Object);
            _bus
                .Setup(b => b.Send(It.IsAny<object>()))
                .Returns(Task.CompletedTask);

            await CreateSut().Execute(input);

            _rentalRepo.Verify(
                r => r.AddAsync(It.Is<Rental>(rental =>
                    rental.VehicleId == vehicleId &&
                    rental.CustomerId == "customer1" &&
                    rental.StartDate == start &&
                    rental.EndDate == end &&
                    rental.ReturnedAt == null)),
                Times.Once);
            _outputPort.Verify(o => o.StandardHandle(It.IsAny<RentVehicleOutput>()), Times.Once);
        }

        private RentVehicleUseCase CreateSut() =>
            new(_vehicleRepo.Object, _rentalRepo.Object, _busFactory.Object, _outputPort.Object);
    }
}
