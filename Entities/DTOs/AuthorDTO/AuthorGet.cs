namespace LibraryAPI.Entities.DTOs.AuthorDTO
{
    public class AuthorGet
    {
        // Unique identifier for the author
        public int? Id { get; set; }

        // Name of the author
        public string? AuthorName { get; set; } = string.Empty;

        // Biography of the author
        public string? Biography { get; set; } = string.Empty;

        // Year of birth of the author
        public short? DateOfBirth { get; set; }

        // Year of death of the author
        public short? DateOfDeath { get; set; }

        // List of book titles associated with the author
        public List<string>? Books { get; set; } = [];

        // Date when the author record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the author record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the author record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State or status of the author record (e.g., active, inactive)
        public string? State { get; set; }
    }
}