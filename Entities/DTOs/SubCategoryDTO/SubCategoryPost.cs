using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.SubCategoryDTO
{
    public class SubCategoryPost
    {
        // Name of the sub-category
        [Required]
        [StringLength(100)]
        public string SubCategoryName { get; set; } = string.Empty;

        // Identifier for the parent category
        [Required]
        public int CategoryId { get; set; }
    }
}