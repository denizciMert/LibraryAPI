using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.LoanDTO
{
    public class LoanPost
    {
        [Required]
        public string MemberId { get; set; } = string.Empty;
        
        [Required] 
        public int EmployeeId { get; set; }
        
        [Required]
        public int BookId { get; set; }
        
        [Required]
        public short CopyNo { get; set; }
    }
}
