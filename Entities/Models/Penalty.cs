using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Penalty model class, inheriting from AbstractEntity.
    // Represents a penalty or fine imposed on a member for overdue items or other violations.
    public class Penalty : AbstractEntity
    {
        // Foreign key for the PenaltyType entity.
        // Indicates the type of penalty (e.g., overdue, lost item).
        public int PenaltyTypeId { get; set; }

        // Navigation property for the PenaltyType entity.
        // Establishes a relationship with the PenaltyType model.
        [ForeignKey(nameof(PenaltyTypeId))]
        public PenaltyType? PenaltyType { get; set; }

        // Foreign key for the Member entity.
        // Represents the member who is penalized.
        [Column(TypeName = "nvarchar(450)")]
        public string PenaltiedMembeId { get; set; } = string.Empty;

        // Navigation property for the Member entity.
        // Establishes a relationship with the Member model.
        [ForeignKey(nameof(PenaltiedMembeId))]
        public Member? Member { get; set; }

        // Foreign key for the Loan entity.
        // Links the penalty to a specific loan.
        public int LoanId { get; set; }

        // Navigation property for the Loan entity.
        // Establishes a relationship with the Loan model.
        [ForeignKey(nameof(LoanId))]
        public Loan? Loan { get; set; }

        // Indicates whether the penalty is currently active.
        // Default value is true, meaning the penalty is active unless otherwise specified.
        public bool Active { get; set; } = true;
    }
}