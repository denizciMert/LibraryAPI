namespace LibraryAPI.Entities.DTOs.SubCategoryDTO
{
    public class SubCategoryGet
    {
        public int? Id { get; set; }
        public string? SubCategoryName { get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
