namespace LibraryAPI.Entities.DTOs.TitleDTO
{
    public class TitleGet
    {
        public int? Id { get; set; }
        public string? TitleName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
