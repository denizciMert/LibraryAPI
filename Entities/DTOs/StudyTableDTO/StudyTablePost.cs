using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.StudyTableDTO
{
    public class StudyTablePost
    {
        [Required]
        [StringLength(6, MinimumLength = 3)]
        public string TableCode { get; set; } = string.Empty;
    }
}
