using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Services.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;

        public ChapterService(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        public Response<bool> AddChapter(int bookId, Chapter chapter)
        {
            var chapters = _chapterRepository.GetChaptersAsync(bookId).Result;
            if (chapters != null)
            {
                var chapterCount = chapters.Chapter.Count + 1;
                chapter.ChapterId = chapterCount;
                chapters.Chapter.Add(chapter);
                _chapterRepository.UpdateChaptersAsync(chapters);
                return new Response<bool>("Chapter correct added", true, System.Net.HttpStatusCode.OK);
            }
            Chapters newChapters = new Chapters(bookId);
            chapter.ChapterId = 1;
            newChapters.Chapter.Add(chapter);
            _chapterRepository.CreateChaptersAsync(newChapters);
            return new Response<bool>("Chapters added, chapter added", true, System.Net.HttpStatusCode.OK);
        }
    }
}
