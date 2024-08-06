using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.PublisherDTO
{
    public class PublisherPost
    {
        // Name of the publisher
        [Required]
        [StringLength(100)]
        public string PublisherName { get; set; } = string.Empty;

        // Phone number of the publisher
        [Phone]
        [StringLength(15, MinimumLength = 7)]
        public string? Phone { get; set; } = string.Empty;

        // Email address of the publisher
        [EmailAddress]
        [StringLength(320, MinimumLength = 3)]
        public string? Email { get; set; } = string.Empty;

        // Contact person for the publisher
        [StringLength(100)]
        public string? ContactPerson { get; set; } = string.Empty;
    }
}