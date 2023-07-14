using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IBookService
    {
        Response<bool> AddChapterToBook(int bookId, AddChapterViewModel model);
        Response<bool> CreateNewBook(CreateNewBookViewModel Book, string ImagePath);
        Response<Book> GetBookById(int bookId);
        Response<List<List<Book>>> GetBooksCompilation();
    }
}
