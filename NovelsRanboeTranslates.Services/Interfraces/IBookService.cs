using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IBookService 
    {
        Response<bool> CreateNewBook(CreateNewBookViewModel Book, string ImagePath);
        Response<List<Book>> GetLatestBooks();
        Response<Book> GetBookById(int bookId);
        Response<List<Book>> GetBestBooksByGenre();
    }
}
