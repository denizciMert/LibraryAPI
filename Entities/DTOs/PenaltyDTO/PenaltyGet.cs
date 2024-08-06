namespace LibraryAPI.Entities.DTOs.PenaltyDTO
{
    public class PenaltyGet
    {
        // Unique identifier for the penalty
        public int? Id { get; set; }

        // Name of the member who incurred the penalty
        public string MemberName { get; set; } = string.Empty;

        // Username of the member who incurred the penalty
        public string UserName { get; set; } = string.Empty;

        // Title of the book associated with the penalty
        public string BookTitle { get; set; } = string.Empty;

        // ISBN of the book associated with the penalty
        public string Isbn { get; set; } = string.Empty;

        // Copy number of the book associated with the penalty
        public short CopyNo { get; set; }

        // Type of penalty applied
        public string PenaltyType { get; set; } = string.Empty;

        // Amount of the penalty
        public float PenaltyAmount { get; set; }

        // Indicates whether the penalty is active or not
        public bool Active { get; set; }

        // Date when the penalty was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the penalty was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the penalty was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State of the penalty (e.g., pending, paid)
        public string? State { get; set; }
    }
}