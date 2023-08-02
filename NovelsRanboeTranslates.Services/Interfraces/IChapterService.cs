using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IChapterService
    {
        public Response<bool> AddChapter(int bookId, Chapter chapter);
        //Response<ChaptersDTO> GetChaptersDTO(int bookId);
        Task<Response<ChaptersDTO>> GetChaptersDTOAsync(int bookId);
    }
}
