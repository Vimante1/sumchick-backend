using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IUserService
    {
        Response<bool> AddPurchased(BuyChapterViewModel model, string userName, decimal chapterPrice);
        Response<User> CreateNewUser(RegistrationViewModel user);
        Response<User> GetBaseUserByLogin(string login);
        Response<UserDTO> GetUserByLogin(string login);
        Response<User> Login(AuthorizationViewModel user);
        Task<bool> AddToBalance(string login, decimal value);
    }
}
