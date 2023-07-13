using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IUserService
    {
        Response<User> CreateNewUser(RegistrationViewModel user);
        Response<User> GetUserByLogin(string login);
    }
}
