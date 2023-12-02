using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface ICommentsService
    {
        public Response<bool> AddComment(int bookId, Comment comment);
        public Task<Response<Comments>> GetCommentsAsync(int bookId);
        public Task<bool> IsUserCommentExist(int bookId, string userName);
    }
}
