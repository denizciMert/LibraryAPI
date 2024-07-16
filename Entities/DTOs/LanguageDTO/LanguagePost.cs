using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.LanguageDTO
{
    public class LanguagePost
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string LanguageName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LanguageCode { get; set; } = string.Empty;
    }
}
