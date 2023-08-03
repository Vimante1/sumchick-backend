using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Services.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public Response<bool> AddComment(int bookId, Comment comment)
        {
            var comments = _commentsRepository.GetCommentsAsync(bookId).Result;
            if (comments != null)
            {
                comments.Comment.Add(comment);
                _commentsRepository.UpdateCommentsAsync(comments);
                return new Response<bool>("Comments correct added", true, System.Net.HttpStatusCode.OK);
            }
            Comments newComments = new Comments(bookId);
            newComments.Comment.Add(comment);
            _commentsRepository.CreateCommentsAsync(newComments);
            return new Response<bool>("Comments added, comment added", true, System.Net.HttpStatusCode.OK);
        }

        public async Task<Response<Comments>> GetCommentsAsync(int bookId)
        {
            var comments = await _commentsRepository.GetCommentsAsync(bookId);

            if (comments != null)
            {
                return new Response<Comments>("Correct", comments, System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new Response<Comments>("CommentsNotFound", null, System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
