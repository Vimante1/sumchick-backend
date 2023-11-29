using Microsoft.AspNetCore.Mvc;
using NovelsRanboeTranslates.Domain.ViewModels;
using NovelsRanboeTranslates.Services.Interfaces;

namespace NovelsRanboeTranslates.Controllers;

[Route("Poster")]
public class PosterController : ControllerBase
{
    public readonly IPosterService _service;

    public PosterController(IPosterService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("CreatePoster")]
    public async Task<IActionResult> CreatePoster(PosterViewModel viewModel)
    {
        return Ok(await _service.CreatePoster(viewModel._id, viewModel.Image));
    }
    [HttpGet]
    [Route("GetPosterList")]
    public async Task<IActionResult> GetPosterList()
    {
        return Ok(await _service.GetPostersList());
    }
    [HttpDelete] 
    [Route("DeletePoster")]
    public async Task<IActionResult> DeletePoster(int posterId)
    {
        return Ok(await _service.DeletePoster(posterId));
    }
}