namespace LibraryAPI.Entities.DTOs.PublisherDTO
{
    public class PublisherGet
    {
        public int? Id { get; set; }
        public string? PublisherName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? ContactPerson { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
