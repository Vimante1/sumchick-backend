using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
