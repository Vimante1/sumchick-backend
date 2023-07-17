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

            var checkName = _repository.GetUserByLogin(user.Password);
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
                    return new Response<User>("The password is not valid", null, System.Net.HttpStatusCode.Unauthorized);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong in user service with login" + ex);
                return new Response<User>("Something wrong in user service with login ", null, System.Net.HttpStatusCode.Unauthorized);
            }
        }

        public Response<bool> BuyChapter(string userLogin, BuyChapterViewModel model, Book book)
        {
            var user = _repository.GetUserByLogin(userLogin).Result;

            if (user == null)
            {
                return new Response<bool>(userLogin + " not found", false, System.Net.HttpStatusCode.NotFound);
            }
            var chapterCost = book.Chapters.Find(c => c.ID == model.ChapterId).Price;
            if (user.Balance < chapterCost)
            {
                return new Response<bool>(userLogin + " not enough money", false, System.Net.HttpStatusCode.BadRequest);
            }
            if (user.Purchased == null)
            {
                user.Purchased = new List<Purchased>();
            }
            var purchasedBook = user.Purchased.Find(p => p.BookID.Equals(model.BookId));
            if (purchasedBook != null)
            {
                purchasedBook.ChapterID.Add(model.ChapterId);
            }
            else
            {
                user.Purchased.Add(new Purchased(model.BookId, model.ChapterId));
            }
            user.Balance -= chapterCost;
            _repository.ReplaceUserByLogin(userLogin, user);
            return new Response<bool>("Correct", true, System.Net.HttpStatusCode.OK);
        }
    }
}
