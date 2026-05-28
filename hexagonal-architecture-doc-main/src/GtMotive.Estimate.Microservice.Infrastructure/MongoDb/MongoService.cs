using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Service that provides access to the MongoDB client and database.
    /// </summary>
    public class MongoService
    {
        private readonly string _databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoService"/> class.
        /// </summary>
        /// <param name="options">MongoDB settings.</param>
        public MongoService(IOptions<MongoDbSettings> options)
        {
            MongoClient = new MongoClient(options.Value.ConnectionString);
            _databaseName = options.Value.MongoDbDatabaseName;
        }

        /// <summary>
        /// Gets the underlying MongoDB client.
        /// </summary>
        public MongoClient MongoClient { get; }

        /// <summary>
        /// Gets the configured MongoDB database.
        /// </summary>
        /// <returns>The <see cref="IMongoDatabase"/> instance.</returns>
        public IMongoDatabase GetDatabase()
        {
            return MongoClient.GetDatabase(_databaseName);
        }
    }
}
