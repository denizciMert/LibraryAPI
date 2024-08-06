using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.CityDTO
{
    public class CityPost
    {
        // The name of the city, required and must be between 4 and 100 characters.
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CityName { get; set; } = string.Empty;

        // The ID of the country where the city is located, required.
        [Required]
        public int CountryId { get; set; }
    }
}