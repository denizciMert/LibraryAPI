using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.DistrictDTO
{
    public class DistrictPost
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string District { get; set; } = string.Empty;

        [Required]
        public int CityId { get; set; }
    }
}
