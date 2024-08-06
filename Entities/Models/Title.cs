using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Title model class, inheriting from AbstractEntity.
    // Represents a title entity in the library system, which could be a book title or other relevant title.
    public class Title : AbstractEntity
    {
        // Name of the title, e.g., "The Great Gatsby", "To Kill a Mockingbird", etc.
        // Column type is varchar with a maximum length of 100 characters.
        [Column(TypeName = "varchar(100)")]
        public string TitleName { get; set; } = string.Empty;
    }
}