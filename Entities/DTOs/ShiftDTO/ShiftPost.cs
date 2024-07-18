using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.ShiftDTO
{
    public class ShiftPost
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string ShiftType { get; set; }
    }
}
