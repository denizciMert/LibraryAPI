using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.Models
{
    // BookLanguage model class that represents the relationship between books and languages.
    public class BookLanguage
    {
        // Foreign key for the Language entity, representing the language of the book.
        [Required]
        public int LanguagesId { get; set; }

        // Navigation property for the Language entity, linking to the Language based on LanguagesId.
        [ForeignKey(nameof(LanguagesId))]
        public Language? Language { get; set; }

        // Foreign key for the Book entity, representing the book associated with this language.
        [Required]
        public int BooksId { get; set; }

        // Navigation property for the Book entity, linking to the Book based on BooksId.
        [JsonIgnore] // Prevents the serialization of the Book property to avoid circular references.
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}