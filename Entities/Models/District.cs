using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // District model class that extends the AbstractEntity class.
    // Represents a district entity within a city in the library system.
    public class District : AbstractEntity
    {
        // Column attribute specifies the data type and length for the DistrictName property.
        [Column(TypeName = "varchar(50)")]
        public string DistrictName { get; set; } = string.Empty;

        // Foreign key property for linking to a City entity.
        public int CityId { get; set; }

        // Navigation property to the related City entity.
        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
    }
}