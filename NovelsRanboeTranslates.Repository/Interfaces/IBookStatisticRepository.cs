using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces;

public interface IBookStatisticRepository
{
    Task<bool> Exists(int bookId);
    Task<bool> Create(BookStatistic bookStatistic);
    Task<BookStatistic> GetOneBookStatistic(int bookId);
    Task<bool> AddToTotalEarnings(int bookId, int addedValue);
    Task<bool> UpdateOneChapterStatistic(int bookId, ChaptersStatistic chaptersStatistic);
    Task<ChaptersStatistic> GetOneChapterStatistic(int bookId, int chapterId);
    Task<bool> AddOneChapterToBook(int bookId, ChaptersStatistic chaptersStatistic);
}