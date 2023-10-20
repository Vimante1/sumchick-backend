using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        bool ReplaceBookById(int bookId, Book newBook);
        Task<bool> UpdateBook(UpdateBookViewModel updateBook);
        Task<List<Book>> GetBestBooksByGenreAsync(List<string> genres);
        Task<List<Book>> GetLatestBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<bool> UpdateLikedPercentBookAsync(Book book, int likedPercent);
        Task<List<BookSearchDTO>> SearchBookByName(string name);
        Task<List<Book>> AdvancedSearch(string originalLanguage, int sortType, string[] genres, int skipCounter);
        void AddViewToBookById(int bookId);
        Task<List<Book>> GetAllBooks();
    }
}
