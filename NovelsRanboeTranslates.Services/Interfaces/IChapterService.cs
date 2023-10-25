using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IChapterService
    {
        public Response<bool> AddChapter(int bookId, Chapter chapter);
        Task<Response<Chapters>> GetChaptersAsync(int bookId);
        public Task<Response<ChaptersDTO>> GetChaptersDTOAsync(int bookId);
        public Task<bool> UpdateOneChapter(int bookId, Chapter updateChapter);
        public Task<Chapter> GetOneChapter(int bookId, int chapterId);
    }
}
