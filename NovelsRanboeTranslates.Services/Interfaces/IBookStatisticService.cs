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
        Task<bool> AddPurchasedChapter(int bookId, int chapterId, decimal price);
        Task<List<BookStatistic>> GetStatisticList();
        void AddOneReadToChapterCounter(int bookId, int chapterId);
    }
}
