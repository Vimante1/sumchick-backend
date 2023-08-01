using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IUserService
    {
        Response<User> CreateNewUser(RegistrationViewModel user);
        Response<UserDTO> GetUserByLogin(string login);
        Response<User> Login(AuthorizationViewModel user);
    }
}
