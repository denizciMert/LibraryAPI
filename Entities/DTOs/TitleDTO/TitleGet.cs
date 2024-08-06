namespace LibraryAPI.Entities.DTOs.TitleDTO
{
    public class TitleGet
    {
        // Unique identifier for the title
        public int? Id { get; set; }

        // Name of the title
        public string? TitleName { get; set; } = string.Empty;

        // Date when the record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // Current state of the record
        public string? State { get; set; }
    }
}