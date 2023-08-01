using MongoDB.Driver;
using NovelsRanboeTranslates.Repository.Interfaces;

namespace NovelsRanboeTranslates.Repository
{
    public class BaseRepository<T>
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDbSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(collectionName);
        }
    }
}

