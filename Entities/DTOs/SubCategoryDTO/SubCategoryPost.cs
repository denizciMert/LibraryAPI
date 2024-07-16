using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.SubCategoryDTO
{
    public class SubCategoryPost
    {
        [Required]
        [StringLength(100)]
        public string SubCategoryName { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
    }
}
