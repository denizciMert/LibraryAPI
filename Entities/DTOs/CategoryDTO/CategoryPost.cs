using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LibraryAPI.Entities.DTOs.CategoryDTO
{
    public class CategoryPost
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
