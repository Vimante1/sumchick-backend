using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        List<Book> GetBestBooksByGenre(List<string> genres);
        List<Book> GetLatestBooks();
        Book GetBookById(int bookId);
    }
}
