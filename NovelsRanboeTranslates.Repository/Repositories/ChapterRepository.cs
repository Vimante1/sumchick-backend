using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                _collection.InsertOneAsync(chapters);
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
    }
}
