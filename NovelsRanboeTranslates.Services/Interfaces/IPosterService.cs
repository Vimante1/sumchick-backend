using Microsoft.AspNetCore.Http;
using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Services.Interfaces;

public interface IPosterService
{
    public Task<List<Poster>> GetPostersList();
    public Task<bool> CreatePoster(int id, IFormFile image);
    public Task<bool> DeletePoster(int posterId);
}