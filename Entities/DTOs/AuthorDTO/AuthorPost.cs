using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.AuthorDTO
{
    public class AuthorPost
    {
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Biography { get; set; } = string.Empty;

        [Required]
        [Range(-4000, 2100)]
        public short DateOfBirth { get; set; } = 0;

        [Range(-4000, 2100)]
        public short? DateOfDeath { get; set; }
    }
}
