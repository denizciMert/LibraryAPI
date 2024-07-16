using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.ReservationDTO
{
    public class ReservationPost
    {
        [Required]
        public string MemberId { get; set; } = string.Empty;

        [Required]
        public int TableId { get; set; }

        [Required]
        public DateTime ReservationStart { get; set; }
    }
}
