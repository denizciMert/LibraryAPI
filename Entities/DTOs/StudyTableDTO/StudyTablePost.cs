using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.StudyTableDTO
{
    public class StudyTablePost
    {
        // Code or identifier for the table, must be between 3 and 6 characters
        [Required]
        [StringLength(6, MinimumLength = 3)]
        public string TableCode { get; set; } = string.Empty;
    }
}