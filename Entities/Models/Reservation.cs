using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Reservation : AbstractEntity
    {
        public DateTime ReservationStart { get; set; }

        public DateTime ReservationEnd { get; set; }

        public string MemberId { get; set; } = string.Empty;

        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }

        public int TableId { get; set; }

        [ForeignKey(nameof(TableId))]
        public StudyTable? StudyTable { get; set; }
    }
}
