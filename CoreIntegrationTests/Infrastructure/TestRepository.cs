using Microsoft.Extensions.Configuration;
using MongoDbGenericRepository;
using System;
using System.IO;

namespace CoreIntegrationTests.Infrastructure
{
    public interface ITestRepository<TKey> : IBaseMongoRepository<TKey> where TKey : IEquatable<TKey>
    {
        void DropTestCollection<TDocument>();
        void DropTestCollection<TDocument>(string partitionKey);
    }

    public static class TestRepositoryFactory
    {
        public static IConfiguration Configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Local.json", false)
                .Build();

        public static string ConnectionString => Configuration.GetConnectionString("MongoDbTests") ?? "mongodb://localhost:27017";
        public static string DatabaseName => Configuration["DatabaseName"];
    }

    public class TestTKeyRepository<TKey> : BaseMongoRepository<TKey>, ITestRepository<TKey> where TKey : IEquatable<TKey>
    {
        static string connectionString => TestRepositoryFactory.ConnectionString;
        static string databaseName => TestRepositoryFactory.DatabaseName;
        private static readonly ITestRepository<TKey> _instance = new TestTKeyRepository<TKey>(connectionString, databaseName);
        /// <inheritdoc />
        private TestTKeyRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public static ITestRepository<TKey> Instance
        {
            get
            {
                return _instance;
            }
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }
    }

    /// <summary>
    /// A singleton implementation of the TestRepository
    /// </summary>
    public sealed class TestRepository : BaseMongoRepository, ITestRepository
    {

        static string connectionString = TestRepositoryFactory.ConnectionString;
        static string databaseName = TestRepositoryFactory.DatabaseName;
        private static readonly ITestRepository _instance = new TestRepository(connectionString, databaseName);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static TestRepository()
        {
        }

        /// <inheritdoc />
        private TestRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public static ITestRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }
    }
}
