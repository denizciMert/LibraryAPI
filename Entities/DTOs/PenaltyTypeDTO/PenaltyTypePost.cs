using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.PenaltyTypeDTO
{
    public class PenaltyTypePost
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string PenaltyType { get; set; } = string.Empty;

        [Required]
        public float AmountToPay { get; set; }
    }
}
