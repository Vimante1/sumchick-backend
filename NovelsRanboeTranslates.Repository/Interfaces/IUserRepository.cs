using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Response<User> GetUserByLogin(string Name);
    }
}
