using NovelsRanboeTranslates.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelsRanboeTranslates.Domain.DTOs
{
    public class ChaptersDTO
    {
        public int Id { get; set; }
        public List<ChapterDTO> Chapter { get; set; }
        public ChaptersDTO(int id, List<ChapterDTO> chapter)
        {
            Id = id;
            Chapter = chapter;
        }
    }

    public class ChapterDTO
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool HasPrice { get; set; }

        public ChapterDTO(Chapter chapter)
        {
            ChapterId = chapter.ChapterId;
            Title = chapter.Title;
            Price = chapter.Price;
            HasPrice = chapter.HasPrice;
        }
    }
}
