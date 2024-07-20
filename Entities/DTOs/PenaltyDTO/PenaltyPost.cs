using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.PenaltyDTO
{
    public class PenaltyPost
    {
        [Required]
        public int PenaltyId { get; set; }

        [Required]
        public string PenaltiedMemberId { get; set; } = string.Empty;

        [Required]
        public int LoanId { get; set; }

        [Required]
        public int PenaltyTypeId { get; set; }
    }
}
