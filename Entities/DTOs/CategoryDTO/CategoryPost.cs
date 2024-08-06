using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.CategoryDTO
{
    public class CategoryPost
    {
        // The name of the category. This field is required and must be between 1 and 100 characters in length.
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
    }
}