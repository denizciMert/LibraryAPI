using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.DTOs.ReservationDTO
{
    public class ReservationPost
    {
        // Unique identifier for the member making the reservation
        [Required]
        public string MemberId { get; set; } = string.Empty;

        // Unique identifier for the employee handling the reservation
        [JsonIgnore]
        public string EmployeeId { get; set; } = string.Empty;

        // Unique identifier for the study table being reserved
        [Required]
        public int TableId { get; set; }

        // Start date and time of the reservation
        [Required]
        public DateTime ReservationStart { get; set; }
    }
}