using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.LocationDTO
{
    public class LocationPost
    {
        [Required]
        [StringLength(6, MinimumLength = 3)]
        public string ShelfCode { get; set; } = string.Empty;
    }
}
