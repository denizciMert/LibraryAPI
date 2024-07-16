namespace LibraryAPI.Entities.DTOs.LoanDTO
{
    public class LoanGet
    {
        public int? Id { get; set; }
        public string? MemberName { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? BookTitle { get; set; } = string.Empty;
        public string? BookIsbn { get; set; } = string.Empty;
        public short? CopyNo { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
