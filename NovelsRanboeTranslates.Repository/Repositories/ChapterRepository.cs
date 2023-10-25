using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using System.Security.Cryptography;
using MongoDB.Bson;

namespace NovelsRanboeTranslates.Repository.Repositories
{
    public class ChapterRepository : BaseRepository<Chapters>, IChapterRepository
    {
        public ChapterRepository(IMongoDbSettings settings) : base(settings, "Chapters") { }

        public Response<bool> Create(Chapters entity)
        {
            throw new NotImplementedException();
        }

        public Response<bool> Delete(Chapters entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateChaptersAsync(Chapters chapters)
        {
            try
            {
                var filter = Builders<Chapters>.Filter.Eq(u => u._id, chapters._id);
                await _collection.ReplaceOneAsync(filter, chapters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateOneChaptersAsync(int bookId, Chapter updateChapter)
        {
            try
            {
                var chaptersFilter = Builders<Chapters>.Filter.Eq("_id", bookId);
                var chapterFilter = Builders<Chapters>.Filter.Eq("Chapter.ChapterId", updateChapter.ChapterId);

                var updateDefinition = Builders<Chapters>.Update
                    .Set("Chapter.$.HasPrice", updateChapter.HasPrice)
                    .Set("Chapter.$.Price", updateChapter.Price)
                    .Set("Chapter.$.Text", updateChapter.Text)
                    .Set("Chapter.$.Title", updateChapter.Title);
                var updateResult = await _collection.UpdateOneAsync(chaptersFilter & chapterFilter, updateDefinition);
                return updateResult.ModifiedCount > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Chapter GetOneChapter(int bookId, int chapterId)
        {
            //TODO: Rewrite
            var filter = Builders<Chapters>.Filter.Eq("_id", bookId);

            var chapters = _collection.Find(filter).FirstOrDefault();

            if (chapters != null)
            {
                return chapters.Chapter.FirstOrDefault(c => c.ChapterId == chapterId);
            }
            else
            {
                return null;
            }

        }


        public async Task<bool> CreateChaptersAsync(Chapters chapters)
        {
            try
            {
                await _collection.InsertOneAsync(chapters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Chapters> GetChaptersAsync(int id)
        {
            try
            {
                var filter = Builders<Chapters>.Filter.Eq("_id", id);
                var chapters = await _collection.Find(filter).FirstOrDefaultAsync();
                return chapters;
            }
            catch { return null; }
        }

        public async Task<ChaptersDTO> GetChaptersDTOAsync(int id)
        {
            try
            {
                var filter = Builders<Chapters>.Filter.Eq("_id", id);
                var chapters = await _collection.Find(filter).FirstOrDefaultAsync();

                if (chapters == null) return null;
                var chapterDtoList = chapters.Chapter.Select(chapter => new ChapterDTO(chapter)).ToList();
                return new ChaptersDTO(id, chapterDtoList);
            }
            catch
            {
                return null;
            }
        }
    }
}
