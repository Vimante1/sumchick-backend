using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;

namespace NovelsRanboeTranslates.Repository.Repositories;

public class PosterRepository : BaseRepository<Poster> , IPosterRepository

{
    public PosterRepository(IMongoDbSettings settings) : base(settings, "Poster") { }
    public async Task<List<Poster>> GetPostersList()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<bool> CreatePoster(Poster poster)
    {
        try
        {
            _collection.InsertOne(poster);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Something with CreatePoster repository \n\n" + e);
            return false;
        }
        
    }

    public async Task<bool> DeletePoster(int posterId)
    {
        var filter = Builders<Poster>.Filter.Eq("_id", posterId);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}