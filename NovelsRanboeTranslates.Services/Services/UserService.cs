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

        public Response<User> CreateNewUser(RegistrationViewModel user)
        {
            User newUser = new() { Login = user.Login, Password = user.Password };

            var checkaName = _repository.GetUserByLogin(user.Password);
            if(checkaName.Result == null) {
                return new Response<User>("User already created ", null, System.Net.HttpStatusCode.BadRequest);
            }
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

        public Response<User> Login(AuthorizationViewModel user)
        {
            try
            {
                var correctUser = _repository.GetUserByLogin(user.Login);
                if (correctUser.Result == null)
                {
                    return correctUser;
                }
                else
                {
                    if (correctUser.Result.Password == user.Password)
                    {
                        return correctUser;
                    }
                    return new Response<User>("ThePasswordIsNotValid", null, System.Net.HttpStatusCode.Unauthorized);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong in user Service with login" + ex);
                return new Response<User>("Something wrong in user Service with login ", null, System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
