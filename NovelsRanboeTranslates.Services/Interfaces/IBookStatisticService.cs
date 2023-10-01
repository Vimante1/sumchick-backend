using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Services.Interfaces
{
    public interface IBookStatisticService
    {
        Task<bool> AddPurchasedChapter(int bookId, int chapterId);
        Task<List<BookStatistic>> GetStatisticListByParameter(int parameter);
    }
}
