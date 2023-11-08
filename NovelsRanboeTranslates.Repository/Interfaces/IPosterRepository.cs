using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces;

public interface IPosterRepository
{
    public Task<List<Poster>> GetPostersList();
    public Task<bool> CreatePoster(Poster poster);
    public Task<bool> DeletePoster(int posterId);

}