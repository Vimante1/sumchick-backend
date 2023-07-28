using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        List<SimpleBookDTO> GetBestBooksByGenre(List<string> genres);
        List<SimpleBookDTO> GetLatestBooks();
        Book GetBookById(int bookId);
        bool ReplaceBookById(int bookId, Book newBook);
    }
}
