using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.LanguageDTO
{
    public class LanguagePost
    {
        // Name of the language, required with length constraints
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LanguageName { get; set; } = string.Empty;

        // Code associated with the language, required with exact length constraint
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string LanguageCode { get; set; } = string.Empty;
    }
}