namespace LibraryAPI.Entities.DTOs.PublisherDTO
{
    public class PublisherGet
    {
        // Unique identifier of the publisher
        public int? Id { get; set; }

        // Name of the publisher
        public string? PublisherName { get; set; } = string.Empty;

        // Phone number of the publisher
        public string? Phone { get; set; } = string.Empty;

        // Email address of the publisher
        public string? Email { get; set; } = string.Empty;

        // Contact person for the publisher
        public string? ContactPerson { get; set; } = string.Empty;

        // Creation date log
        public DateTime? CreatinDateLog { get; set; }

        // Last update date log
        public DateTime? UpdateDateLog { get; set; }

        // Deletion date log
        public DateTime? DeleteDateLog { get; set; }

        // Current state of the publisher
        public string? State { get; set; }
    }
}