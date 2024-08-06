using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // City model class that extends the AbstractEntity class.
    // Represents a city entity within the library system.
    public class City : AbstractEntity
    {
        // Column attribute specifies the data type and length for the CityName property.
        [Column(TypeName = "varchar(100)")]
        public string CityName { get; set; } = string.Empty;

        // Foreign key property representing the identifier of the associated country.
        public int CountryId { get; set; }

        // Navigation property to the associated Country entity.
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }
    }
}