using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.PublisherDTO
{
    public class PublisherPost
    {
        [Required]
        [StringLength(100)]
        public string PublisherName { get; set; } = string.Empty;

        [Phone]
        [StringLength(15, MinimumLength = 7)]
        public string? Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(320, MinimumLength = 3)]
        public string? Email { get; set; } = string.Empty;

        [StringLength(100)]
        public string? ContactPerson { get; set; } = string.Empty;
    }
}
