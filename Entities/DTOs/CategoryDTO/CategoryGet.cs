namespace LibraryAPI.Entities.DTOs.CategoryDTO
{
    public class CategoryGet
    {
        public int? Id { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
