using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.LocationDTO
{
    public class LocationPost
    {
        // Code representing the shelf
        [Required]
        [StringLength(6, MinimumLength = 3)]
        public string ShelfCode { get; set; } = string.Empty;
    }
}