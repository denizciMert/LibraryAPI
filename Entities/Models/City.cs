using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class City : AbstractEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string CityName { get; set; } = string.Empty;

        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }
    }
}
