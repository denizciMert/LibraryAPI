namespace LibraryAPI.Entities.DTOs.ReservationDTO
{
    public class ReservationGet
    {
        public int? Id { get; set; }
        public string? MemberName { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? StudyTable { get; set; } = string.Empty;
        public DateTime? ReservationStart { get; set; }
        public DateTime? ReservationEnd { get; set; }
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
