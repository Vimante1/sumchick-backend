using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using MongoDB.Driver;

namespace NovelsRanboeTranslates.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository()
        {
            var client = new MongoClient("mongodb://root:example@132.226.192.36:27017");
            var database = client.GetDatabase("Translates");
            _collection = database.GetCollection<User>("User");
        }

        public Response<bool> Create(User entity)
        {
            try
            {
                _collection.InsertOne(entity);
                return new Response<bool> ("Correct", true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex) 
            {
                return new Response<bool> (ex.Message, false, System.Net.HttpStatusCode.BadRequest);
            };
        }

        public Response<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Response<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
