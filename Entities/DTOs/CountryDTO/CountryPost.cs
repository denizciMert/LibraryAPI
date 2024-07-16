using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.CountryDTO
{
    public class CountryPost
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CountryName { get; set; } = string.Empty;
    }
}
