using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Reservation model class, inheriting from AbstractEntity.
    // Represents a reservation made by a member for a study table.
    public class Reservation : AbstractEntity
    {
        // The start date and time of the reservation.
        public DateTime ReservationStart { get; set; }

        // The end date and time of the reservation.
        public DateTime ReservationEnd { get; set; }

        // Identifier for the member who made the reservation.
        // Column type is nvarchar with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string MemberId { get; set; } = string.Empty;

        // Navigation property to the Member entity.
        // Represents the member who made the reservation.
        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }

        // Identifier for the employee who handled the reservation.
        // Column type is nvarchar with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string EmployeeId { get; set; } = string.Empty;

        // Navigation property to the Employee entity.
        // Represents the employee who handled the reservation.
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        // Identifier for the study table that was reserved.
        public int TableId { get; set; }

        // Navigation property to the StudyTable entity.
        // Represents the study table that was reserved.
        [ForeignKey(nameof(TableId))]
        public StudyTable? StudyTable { get; set; }

        // Indicates whether the reservation is active.
        // Defaults to true, meaning the reservation is active unless otherwise specified.
        public bool Active { get; set; } = true;
    }
}