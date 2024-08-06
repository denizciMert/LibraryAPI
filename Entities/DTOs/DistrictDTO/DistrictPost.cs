using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.DistrictDTO
{
    public class DistrictPost
    {
        // Name of the district with validation constraints
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string District { get; set; } = string.Empty;

        // Identifier for the city to which the district belongs
        [Required]
        public int CityId { get; set; }
    }
}