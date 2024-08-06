using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Category model class that extends the AbstractEntity class.
    // Represents a category in the library system.
    public class Category : AbstractEntity
    {
        // Column attribute specifies the data type and length for the CategoryName property.
        [Column(TypeName = "varchar(100)")]
        public string CategoryName { get; set; } = string.Empty;
    }
}