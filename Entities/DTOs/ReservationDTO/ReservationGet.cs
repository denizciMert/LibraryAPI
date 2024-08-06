namespace LibraryAPI.Entities.DTOs.ReservationDTO
{
    public class ReservationGet
    {
        // Unique identifier for the reservation
        public int? Id { get; set; }

        // Name of the member who made the reservation
        public string? MemberName { get; set; } = string.Empty;

        // Username of the member who made the reservation
        public string? UserName { get; set; } = string.Empty;

        // Name of the employee who handled the reservation
        public string? EmployeeName { get; set; } = string.Empty;

        // Name of the study table reserved
        public string? StudyTable { get; set; } = string.Empty;

        // Start date and time of the reservation
        public DateTime? ReservationStart { get; set; }

        // End date and time of the reservation
        public DateTime? ReservationEnd { get; set; }

        // Indicates if the reservation is active or not
        public bool? Active { get; set; }

        // Creation date log of the reservation record
        public DateTime? CreatinDateLog { get; set; }

        // Last update date log of the reservation record
        public DateTime? UpdateDateLog { get; set; }

        // Deletion date log of the reservation record
        public DateTime? DeleteDateLog { get; set; }

        // State of the reservation record
        public string? State { get; set; }
    }
}