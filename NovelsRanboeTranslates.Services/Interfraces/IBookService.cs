using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IBookService
    {
        Response<bool> CreateNewBook(CreateNewBookViewModel Book, string ImagePath);
        Response<List<Book>> GetLatestBooks();
        Response<List<Book>> GetBestBooksByGenre();
        Task<Response<Book>> GetBookByIdAsync(int bookId);
        Task<bool> UpdateLikedPercent(int bookId, int likedPercent);
        Task<List<BookSearchDTO>> SearchBookByName(string name);
        Task<List<Book>> AdvancedSearch(string originalLanguage, int sortType, string[] genres, int skipCounter);
    }
}
