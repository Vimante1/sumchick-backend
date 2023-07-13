using Microsoft.VisualBasic;
using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;

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
                return new Response<bool>("Correct", true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new Response<bool>(ex.Message, false, System.Net.HttpStatusCode.BadRequest);
            };
        }

        public Response<User> GetUserByLogin(string login)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("Login", login);
                var user = _collection.Find(filter).FirstOrDefault();
                if (user != null)
                {
                    return new Response<User>("Correct", user, System.Net.HttpStatusCode.OK);
                }
                else
                {
                    return new Response<User>("UserNotFound", null, System.Net.HttpStatusCode.NotFound);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Something wrong with find in repository");
                Console.WriteLine(ex);
                return new Response<User>("UserNotFound", null, System.Net.HttpStatusCode.NotFound);

            }
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
