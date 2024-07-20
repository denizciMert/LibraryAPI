using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Book : AbstractEntity
    {
        [Column(TypeName = "Varchar(13)")]
        public string? Isbn { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public short PageCount { get; set; } = 1;
        
        public short DateOfPublish { get; set; } = 0;

        public short CopyCount { get; set; } = 1;

        public bool Banned { get; set; }

        public float Rating { get; set; }

        public int PublisherId { get; set; }

        public int LocationId { get; set; }

        [ForeignKey(nameof(PublisherId))]
        public Publisher? Publisher { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location? Location { get; set; }

        public List<BookSubCategory>? BookSubCategories { get; set; }

        public List<BookLanguage>? BookLanguages { get; set; }

        public List<AuthorBook>? AuthorBooks { get; set; }

        public List<BookCopy>? BookCopies { get; set; }

        public string? BookImagePath { get; set; } = string.Empty;
    }
}
