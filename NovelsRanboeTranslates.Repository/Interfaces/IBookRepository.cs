using Amazon.SecurityToken.Model;
using NovelsRanboeTranslates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        List<Book> GetBestBooksByGenre(List<string> genres);
        List<Book> GetLatestBooks();
    }
}
