using System.ComponentModel.DataAnnotations;

namespace NovelsRanboeTranslates.Domain.ViewModels
{
    public class BuyChapterViewModel
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int ChapterId { get; set; }
    }
}
