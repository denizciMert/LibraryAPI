namespace LibraryAPI.Entities.DTOs.StudyTableDTO
{
    public class StudyTableGet
    {
        public int? Id { get; set; }
        public string? TableCode { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
