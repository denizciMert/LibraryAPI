namespace LibraryAPI.Entities.DTOs.CategoryDTO
{
    public class CategoryGet
    {
        // The unique identifier for the category. This is nullable in case the category is not yet created.
        public int? Id { get; set; }

        // The name of the category. Defaults to an empty string if not provided.
        public string? CategoryName { get; set; } = string.Empty;

        // The date and time when the category was created. Nullable in case the creation date is not set.
        public DateTime? CreatinDateLog { get; set; }

        // The date and time when the category was last updated. Nullable in case the update date is not set.
        public DateTime? UpdateDateLog { get; set; }

        // The date and time when the category was deleted. Nullable in case the delete date is not set.
        public DateTime? DeleteDateLog { get; set; }

        // The state of the category (e.g., active, inactive). Nullable in case the state is not set.
        public string? State { get; set; }
    }
}