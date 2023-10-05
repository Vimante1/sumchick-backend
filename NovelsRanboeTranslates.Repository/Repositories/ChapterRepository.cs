using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.DTOs;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;

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
