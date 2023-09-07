﻿using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        bool ReplaceBookById(int bookId, Book newBook);
        Task<List<Book>> GetBestBooksByGenreAsync(List<string> genres);
        Task<List<Book>> GetLatestBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<bool> UpdateLikedPercentBookAsync(Book book, int likedPercent);
        Task<List<BookSearchDTO>> SearchBookByName(string name);
        Task<List<Book>> AdvancedSearch(string originalLanguage, int sortType, string[] genres, int skipCounter);
    }
}
