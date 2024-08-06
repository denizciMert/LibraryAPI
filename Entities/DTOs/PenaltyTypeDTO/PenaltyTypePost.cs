using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.PenaltyTypeDTO
{
    public class PenaltyTypePost
    {
        // Name of the penalty type
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string? PenaltyType { get; set; } = string.Empty;

        // Amount to be paid for the penalty
        [Required]
        public float AmountToPay { get; set; }
    }
}