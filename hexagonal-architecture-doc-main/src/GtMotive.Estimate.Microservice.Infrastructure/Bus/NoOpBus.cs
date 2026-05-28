using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Bus
{
    /// <summary>
    /// No-operation bus implementation used when no real message broker is configured.
    /// </summary>
    public sealed class NoOpBus : IBus
    {
        /// <inheritdoc/>
        public Task Send(object message)
        {
            return Task.CompletedTask;
        }
    }
}
