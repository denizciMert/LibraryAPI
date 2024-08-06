namespace LibraryAPI.Entities.DTOs.ShiftDTO
{
    public class ShiftGet
    {
        // Unique identifier for the shift
        public int? Id { get; set; }

        // Type or description of the shift
        public string? ShiftType { get; set; } = string.Empty;

        // Date and time when the shift record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date and time when the shift record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date and time when the shift record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // Current state of the shift (e.g., active, inactive)
        public string? State { get; set; }
    }
}