using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface ICommentsRepository : IBaseRepository<Comments>
    {
        public Task<bool> CreateCommentsAsync(Comments comments);
        public Task<bool> UpdateCommentsAsync(Comments comments);
        public Task<Comments> GetCommentsAsync(int id);

    }
}
