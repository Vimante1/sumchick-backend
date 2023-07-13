using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        private static Random random = new Random();

        public Response<User> CreateNewUser(RegistrationViewModel user)
        {
            User newUser = new User() { Login = user.Login, Password = user.Password };

            if (_repository.Create(newUser).Result)
            {
                return new Response<User>("User created", newUser, System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new Response<User>("User not created", newUser, System.Net.HttpStatusCode.BadRequest);
            };
        }

        public Response<User> GetUserByLogin(string login)
        {
            var user = _repository.GetUserByLogin(login);
            return user;
        }
    }
}
