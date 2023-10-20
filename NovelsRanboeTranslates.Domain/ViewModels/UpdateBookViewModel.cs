using System.ComponentModel.DataAnnotations;

namespace NovelsRanboeTranslates.Domain.ViewModels;

public class UpdateBookViewModel
{
    [Required]
    public int _id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string OriginalLanguage { get; set; }
    [Required]
    public string[] Genre { get; set; }
}