using System;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Bus
{
    /// <summary>
    /// No-operation bus factory that returns a <see cref="NoOpBus"/> for any event type.
    /// </summary>
    public sealed class NoOpBusFactory : IBusFactory
    {
        private static readonly NoOpBus BusInstance = new();

        /// <inheritdoc/>
        public IBus GetClient(Type eventType)
        {
            return BusInstance;
        }
    }
}
