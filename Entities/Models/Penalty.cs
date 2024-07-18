using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Penalty : AbstractEntity
    {
        public int PenaltyTypeId { get; set; }

        [ForeignKey(nameof(PenaltyTypeId))]
        public PenaltyType? PenaltyType { get; set; }

        public string PenaltiedMembeId { get; set; } = string.Empty;

        [ForeignKey(nameof(PenaltiedMembeId))] 
        public Member? Member { get; set; }

        public int LoanId { get; set; }

        [ForeignKey(nameof(LoanId))]
        public Loan? Loan { get; set; }

        public bool Active { get; set; } = true;
    }
}
