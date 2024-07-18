using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.TitleDTO
{
    public class TitlePost
    {
        [Required]
        [StringLength(100)]
        public string TitleName { get; set; } = string.Empty;
    }
}
