using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Location model class.
    // Represents the location of a book within the library, inheriting from AbstractEntity.
    public class Location : AbstractEntity
    {
        // Code indicating the shelf or location where the book is stored.
        // Column type is varchar(6) to accommodate shelf codes up to 6 characters long.
        [Column(TypeName = "varchar(6)")]
        public string ShelfCode { get; set; } = string.Empty;
    }
}