namespace LibraryAPI.Entities.DTOs.PenaltyDTO
{
    public class PenaltyGet
    {
        public int? Id { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public short CopyNo { get; set; }
        public string PenaltyType { get; set; } = string.Empty;
        public float PenaltyAmount { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
