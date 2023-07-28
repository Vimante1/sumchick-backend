using MongoDB.Bson;
using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IBookService
    {
        Response<bool> AddChapterToBook(int bookId, AddChapterViewModel model);
        Response<bool> AddCommentToBook(int bookId, Comment comment);
        Response<bool> CreateNewBook(CreateNewBookViewModel Book, string ImagePath);
        Response<List<SimpleBookDTO>> GetBestBooksByGenre();
        Response<List<SimpleBookDTO>> GetLatestBooks();
        Response<Book> GetBookById(int bookId);
    }
}
