using NovelsRanboeTranslates.Domain.DTOs;
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

            var checkName = _repository.GetUserByLogin(user.Login);
            if (checkName.Result != null)
            {
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

        public Response<UserDTO> GetUserByLogin(string login)
        {
            var user = _repository.GetUserByLogin(login);
            var response = new Response<UserDTO>("Comment", new UserDTO { _id = user.Result._id, Balance = user.Result.Balance, Role = user.Result.Role, FavoritesBookID = user.Result.FavoritesBookID, Login = user.Result.Login, Purchased = user.Result.Purchased }, System.Net.HttpStatusCode.OK);
            return response;
        }

        public Response<User> GetBaseUserByLogin(string login)
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
                    return new Response<User>("The password is not valid", null, System.Net.HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong in user service with login" + ex);
                return new Response<User>("Something wrong in user service with login ", null, System.Net.HttpStatusCode.Unauthorized);
            }
        }

        public Response<bool> AddPurchased(BuyChapterViewModel model, string userName, decimal chapterPrice)
        {
            var user = _repository.GetUserByLogin(userName);
            if (user.Result == null)
            {
                return new Response<bool>(user.Comment, false, System.Net.HttpStatusCode.NotFound);
            }
            if(user.Result.Balance < chapterPrice)
            {
                return new Response<bool>("Not enough money", false, System.Net.HttpStatusCode.NotFound);
            }
            var existingPurchase = user.Result.Purchased.FirstOrDefault(p => p.BookID == model.BookId);
            if (existingPurchase != null)
            {
                existingPurchase.ChapterID.Add(model.ChapterId);
            }
            else
            {
                user.Result.Purchased.Add(new Purchased(model.BookId, model.ChapterId));
            }
            user.Result.Balance -= chapterPrice;
            var result = _repository.ReplaceUserByLogin(userName, user.Result);
            return new Response<bool>("Correct", result, System.Net.HttpStatusCode.OK);

        }
    }
}
