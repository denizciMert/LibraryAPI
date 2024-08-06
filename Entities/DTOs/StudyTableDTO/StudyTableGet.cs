namespace LibraryAPI.Entities.DTOs.StudyTableDTO
{
    public class StudyTableGet
    {
        // Unique identifier for the study table
        public int? Id { get; set; }

        // Code or identifier for the table
        public string? TableCode { get; set; } = string.Empty;

        // Creation date log for the study table
        public DateTime? CreatinDateLog { get; set; }

        // Last update date log for the study table
        public DateTime? UpdateDateLog { get; set; }

        // Deletion date log for the study table, if applicable
        public DateTime? DeleteDateLog { get; set; }

        // State or status of the study table
        public string? State { get; set; } = string.Empty;
    }
}