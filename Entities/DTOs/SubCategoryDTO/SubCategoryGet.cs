namespace LibraryAPI.Entities.DTOs.SubCategoryDTO
{
    public class SubCategoryGet
    {
        // Identifier for the sub-category
        public int? Id { get; set; }

        // Name of the sub-category
        public string? SubCategoryName { get; set; } = string.Empty;

        // Name of the parent category
        public string? CategoryName { get; set; } = string.Empty;

        // Date when the record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // Current state of the record (e.g., Active, Inactive)
        public string? State { get; set; }
    }
}