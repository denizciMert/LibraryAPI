using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Author model class extending AbstractEntity to include common entity properties.
    public class Author : AbstractEntity
    {
        // Full name of the author, stored as a variable-length Unicode string with a maximum length of 1000 characters.
        [Column(TypeName = "Nvarchar(1000)")]
        public string AuthorFullName { get; set; } = string.Empty;

        // Biography of the author, optional and stored as a variable-length Unicode string with a maximum length of 1000 characters.
        [Column(TypeName = "Nvarchar(1000)")]
        public string? Biography { get; set; }

        // Year of birth of the author, represented as a short integer.
        public short DateOfBirth { get; set; }

        // Optional year of death of the author, represented as a short integer.
        public short? DateOfDeath { get; set; }

        // List of AuthorBook entities associated with this author.
        // This represents a many-to-many relationship between authors and books.
        public List<AuthorBook>? AuthorBooks { get; set; }
    }
}