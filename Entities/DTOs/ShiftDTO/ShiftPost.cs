using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.ShiftDTO
{
    public class ShiftPost
    {
        // Type or description of the shift
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string ShiftType { get; set; } = string.Empty;
    }
}