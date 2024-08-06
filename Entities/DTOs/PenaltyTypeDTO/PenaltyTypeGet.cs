namespace LibraryAPI.Entities.DTOs.PenaltyTypeDTO
{
    public class PenaltyTypeGet
    {
        // Unique identifier for the penalty type
        public int? Id { get; set; }

        // Name of the penalty type
        public string? PenaltyName { get; set; } = string.Empty;

        // Amount to be paid for the penalty
        public float? AmountToPay { get; set; }

        // Date when the record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State of the penalty type (e.g., active, inactive)
        public string? State { get; set; }
    }
}