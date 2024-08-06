using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.PenaltyDTO
{
    public class PenaltyPost
    {
        // ID of the member who is being penalized
        [Required]
        public string PenaltiedMemberId { get; set; } = string.Empty;

        // ID of the loan associated with the penalty
        [Required]
        public int LoanId { get; set; }

        // ID of the penalty type
        [Required]
        public int PenaltyTypeId { get; set; }
    }
}