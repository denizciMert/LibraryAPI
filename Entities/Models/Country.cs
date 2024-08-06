using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Country model class that extends the AbstractEntity class.
    // Represents a country entity within the library system.
    public class Country : AbstractEntity
    {
        // Column attribute specifies the data type and length for the CountryName property.
        [Column(TypeName = "varchar(100)")]
        public string CountryName { get; set; } = string.Empty;
    }
}