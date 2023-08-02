using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        bool ReplaceBookById(int bookId, Book newBook);
        Task<List<Book>> GetBestBooksByGenreAsync(List<string> genres);
        Task<List<Book>> GetLatestBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
    }
}
