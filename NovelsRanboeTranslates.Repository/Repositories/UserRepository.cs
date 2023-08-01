using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;

namespace NovelsRanboeTranslates.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(IMongoDbSettings settings) : base(settings, "User") { }
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
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong with find in user repository GetUserByLogin" + ex);
                return new Response<User>("UserNotFound", null, System.Net.HttpStatusCode.NotFound);
            }
        }

        public bool ReplaceUserByLogin(string userLogin, User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("Login", userLogin);
                var result = _collection.ReplaceOneAsync(filter, user);
                if (result != null) { return true; }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Response<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }


    }
}
