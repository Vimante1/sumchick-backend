using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Services.Services;

public class PosterService : IPosterService
{
    private readonly IPosterRepository _repository;

    public PosterService(IPosterRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Poster>> GetPostersList()
    {
        return await _repository.GetPostersList();
    }

    public async Task<bool> CreatePoster(int id, IFormFile image)
    {
        try
        {
            if (image == null || image.Length <= 0) return false;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var localPath = Path.Combine("/app/images/", fileName);
            //var localPath = Path.Combine("D:/", fileName);

            using (var stream = new FileStream(localPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            var publicPath = "http://localhost:8080/images" + fileName;
            var poster = new Poster(id, publicPath);
            return await _repository.CreatePoster(poster);

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception in poster service\n\n" + e);
            return false;
        }
    }
    public async Task<bool> DeletePoster(int posterId)
    {
        return await _repository.DeletePoster(posterId);
    }
}