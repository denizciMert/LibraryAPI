using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.CategoryDTO
{
    public class CategoryPost
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
