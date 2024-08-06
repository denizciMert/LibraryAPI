using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Book model class which extends AbstractEntity to include base entity properties
    public class Book : AbstractEntity
    {
        // ISBN of the book, defined as a variable-length string with a maximum length of 13 characters.
        [Column(TypeName = "Varchar(13)")]
        public string? Isbn { get; set; }

        // Title of the book, defined as a variable-length string with a maximum length of 450 characters.
        [Column(TypeName = "varchar(450)")]
        public string BookTitle { get; set; } = string.Empty;

        // Number of pages in the book, default value is set to 1.
        public short PageCount { get; set; } = 1;

        // Year of publication, default value is set to 0.
        public short DateOfPublish { get; set; } = 0;

        // Number of copies available, default value is set to 1.
        public short CopyCount { get; set; } = 1;

        // Indicates if the book is banned or not.
        public bool Banned { get; set; }

        // Rating of the book, default value is not set (zero or a suitable default).
        public float Rating { get; set; }

        // Foreign key for the Publisher entity.
        public int PublisherId { get; set; }

        // Foreign key for the Location entity.
        public int LocationId { get; set; }

        // Navigation property for the Publisher entity, linking to the Publisher based on PublisherId.
        [ForeignKey(nameof(PublisherId))]
        public Publisher? Publisher { get; set; }

        // Navigation property for the Location entity, linking to the Location based on LocationId.
        [ForeignKey(nameof(LocationId))]
        public Location? Location { get; set; }

        // Collection of BookSubCategory entities, representing the many-to-many relationship between books and subcategories.
        public List<BookSubCategory>? BookSubCategories { get; set; }

        // Collection of BookLanguage entities, representing the many-to-many relationship between books and languages.
        public List<BookLanguage>? BookLanguages { get; set; }

        // Collection of AuthorBook entities, representing the many-to-many relationship between books and authors.
        public List<AuthorBook>? AuthorBooks { get; set; }

        // Collection of BookCopy entities, representing different copies of the same book.
        public List<BookCopy>? BookCopies { get; set; }

        // Path to the book's cover image, defined as a variable-length string with a maximum length of 450 characters.
        [Column(TypeName = "varchar(450)")]
        public string? BookImagePath { get; set; } = string.Empty;
    }
}
