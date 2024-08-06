using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.AuthorDTO
{
    public class AuthorPost
    {
        // The name of the author. This field is required and must not exceed 100 characters.
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = string.Empty;

        // A brief biography of the author. This field is optional and must not exceed 1000 characters.
        [StringLength(1000)]
        public string? Biography { get; set; } = string.Empty;

        // The year of birth of the author. This field is required and must be between -4000 and 2100.
        [Required]
        [Range(-4000, 2100)]
        public short DateOfBirth { get; set; } = 0;

        // The year of death of the author. This field is optional and must be between -4000 and 2100 if provided.
        [Range(-4000, 2100)]
        public short? DateOfDeath { get; set; }
    }
}