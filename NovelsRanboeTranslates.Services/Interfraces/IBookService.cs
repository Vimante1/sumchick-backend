using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IBookService
    {
        Response<bool> CreateNewBook(CreateNewBookViewModel Book, string ImagePath);
        Response<List<List<Book>>> GetBooksCompilation();
    }
}
