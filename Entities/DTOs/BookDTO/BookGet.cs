namespace LibraryAPI.Entities.DTOs.BookDTO
{
    public class BookGet
    {
        public int? Id { get; set; }
        public string? Isbn { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public short? PageCount { get; set; }
        public short? DateOfPublish { get; set; }
        public string? Publisher { get; set; } = string.Empty;
        public short? CopyCount { get; set; }
        public string? Location { get; set; } = string.Empty;
        public float? Rating { get; set; }
        public List<string>? Authors { get; set; } = [];
        public List<string>? SubCategories { get; set; } = [];
        public List<string>? Languages { get; set; } = [];
        public List<int>? CopyNumbers { get; set; }
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
