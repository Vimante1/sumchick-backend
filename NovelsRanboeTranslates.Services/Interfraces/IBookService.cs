using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IBookService
    {
        Response<bool> CreateNewBook(CreateNewBookViewModel Book, string ImagePath);
        Response<List<List<Book>>> GetBooksCompilation();
    }
}
