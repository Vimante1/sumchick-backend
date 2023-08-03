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
    public class CommentsRepository : BaseRepository<Comments>, ICommentsRepository
    {
        public CommentsRepository(IMongoDbSettings settings) : base(settings, "Comments") { }

        public Response<bool> Create(Comments entity)
        {
            throw new NotImplementedException();
        }
        public Response<bool> Delete(Comments entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCommentsAsync(Comments comments)
        {
            try
            {
                await _collection.InsertOneAsync(comments);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<Comments> GetCommentsAsync(int id)
        {
            try
            {
                var filter = Builders<Comments>.Filter.Eq("_id", id);
                var chapters = await _collection.Find(filter).FirstOrDefaultAsync();
                return chapters;
            }
            catch { return null; }
        }

        public async Task<bool> UpdateCommentsAsync(Comments comments)
        {
            try
            {
                var filter = Builders<Comments>.Filter.Eq(u => u._id, comments._id);
                await _collection.ReplaceOneAsync(filter, comments);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
