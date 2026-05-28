using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Bus;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    /// <summary>
    /// Extension methods for infrastructure service registration.
    /// </summary>
    public static class InfrastructureConfiguration
    {
        /// <summary>
        /// Registers all base infrastructure services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="isDevelopment">Whether the application is running in development mode.</param>
        /// <returns>An <see cref="IInfrastructureBuilder"/> for further configuration.</returns>
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            services.AddSingleton<IBusFactory, NoOpBusFactory>();
            services.AddSingleton<MongoService>();
            services.AddScoped<IVehicleRepository, MongoVehicleRepository>();
            services.AddScoped<IRentalRepository, MongoRentalRepository>();

            return new InfrastructureBuilder(services);
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
