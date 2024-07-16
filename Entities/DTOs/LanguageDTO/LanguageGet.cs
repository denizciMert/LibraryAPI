namespace LibraryAPI.Entities.DTOs.LanguageDTO
{
    public class LanguageGet
    {
        public int? Id { get; set; }
        public string? LanguageName { get; set; } = string.Empty;
        public string? LanguageCode { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
