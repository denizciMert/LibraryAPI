using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class District : AbstractEntity
    {
        [Column(TypeName = "varchar(50)")]
        public string DistrictName { get; set; } = string.Empty;

        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
    }
}
