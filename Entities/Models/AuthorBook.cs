using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.Models
{
    // AuthorBook cross-reference model class to represent the many-to-many relationship
    // between authors and books.
    public class AuthorBook
    {
        // Foreign key for the Author entity.
        [Required]
        public int AuthorsId { get; set; }

        // Navigation property for the Author entity.
        // This creates a reference to the Author entity using the AuthorsId foreign key.
        [ForeignKey(nameof(AuthorsId))]
        public Author? Author { get; set; }

        // Foreign key for the Book entity.
        [Required]
        public int BooksId { get; set; }

        // Navigation property for the Book entity.
        // This creates a reference to the Book entity using the BooksId foreign key.
        [JsonIgnore] // Prevents serialization of the Book property in JSON output.
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}