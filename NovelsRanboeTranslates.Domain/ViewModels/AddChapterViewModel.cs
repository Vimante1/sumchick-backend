using System.ComponentModel.DataAnnotations;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class AddChapterViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The login must contain from 4 to 50 characters")]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public double Price { get; set; }

    }
}
