using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.CityDTO
{
    public class CityPost
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CityName { get; set; } = string.Empty;

        [Required]
        public int CountryId { get; set; }
    }
}
