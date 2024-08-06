using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.CountryDTO
{
    public class CountryPost
    {
        // Required attribute ensures this field must be provided.
        // StringLength attribute enforces that the name must be between 4 and 100 characters.
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CountryName { get; set; } = string.Empty;
    }
}