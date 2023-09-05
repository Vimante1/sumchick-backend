using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class CreateNewBookViewModel
    {
        [Required(ErrorMessage = "Title is a required field")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Author is a required field")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Original language is a required field")]
        public string OriginalLanguage { get; set; }
        [Required(ErrorMessage = "Genre is a required field")]
        public string[] Genre { get; set; }
        [Required(ErrorMessage = "Image is a required field")]
        public IFormFile Image { get; set; }
    }
}
