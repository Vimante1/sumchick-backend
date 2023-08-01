using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Services.Interfraces
{
    public interface IChapterService
    {
        public Response<bool> AddChapter(int bookId, Chapter chapter);
    }
}
