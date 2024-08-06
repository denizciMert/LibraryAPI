using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.DTOs.BookDTO
{
    public class BookPost
    {
        // The International Standard Book Number (ISBN) of the book. Must be between 10 and 13 characters.
        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string Isbn { get; set; } = string.Empty;

        // The title of the book. Maximum length of 100 characters.
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        // The number of pages in the book. Must be a positive integer.
        [Required]
        [Range(1, short.MaxValue)]
        public short PageCount { get; set; }

        // The year the book was published. Must be between -4000 and 2100.
        [Required]
        [Range(-4000, 2100)]
        public short DateOfPublish { get; set; }

        // List of copy numbers for the book. This field is required.
        [Required]
        public List<int> CopyNumbers { get; set; } = [];

        // The identifier for the publisher of the book. This field is required.
        [Required]
        public int PublisherId { get; set; }

        // The identifier for the location of the book. This field is required.
        [Required]
        public int LocationId { get; set; }

        // List of identifiers for the subcategories the book belongs to. This field is required.
        [Required]
        public List<int> SubCategoryIds { get; set; } = [];

        // List of identifiers for the languages the book is available in. This field is required.
        [Required]
        public List<int> LanguageIds { get; set; } = [];

        // List of identifiers for the authors of the book. This field is required.
        [Required]
        public List<int> AuthorIds { get; set; } = [];

        // The path to the book's image. This field is optional.
        public string? ImagePath { get; set; } = string.Empty;

        // Form file for the book's image. This field is not mapped to the database.
        [NotMapped]
        public IFormFile? FileForm { get; set; }
    }
}
