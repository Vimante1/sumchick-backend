using Microsoft.AspNetCore.Http;

namespace NovelsRanboeTranslates.Domain.ViewModels;

public class PosterViewModel
{
    public int _id { get;set; }
    public IFormFile Image { get; set; }
}