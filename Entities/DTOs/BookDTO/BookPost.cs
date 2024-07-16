using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.BookDTO
{
    public class BookPost
    {
        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string Isbn { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1, short.MaxValue)]
        public short PageCount { get; set; }

        [Required]
        [Range(-4000, 2100)]
        public short DateOfPublish { get; set; }

        [Required]
        public List<int> CopyNumbers { get; set; } = [];

        [Required]
        public int PublisherId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public List<int> SubCategoryIds { get; set; } = [];

        [Required]
        public List<int> LanguageIds { get; set; } = [];

        [Required]
        public List<int> AuthorIds { get; set; } = [];
    }
}
