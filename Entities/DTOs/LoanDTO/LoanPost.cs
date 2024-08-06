using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.DTOs.LoanDTO
{
    public class LoanPost
    {
        // ID of the member borrowing the book
        [Required]
        public string MemberId { get; set; } = string.Empty;

        // ID of the employee processing the loan
        [JsonIgnore]
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        // ID of the book being borrowed
        [Required]
        public int BookId { get; set; }

        // Copy number of the book being borrowed
        [Required]
        public short CopyNo { get; set; }
    }
}