using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Entities.DTOs.MemberDTO
{
    public class MemberPost
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 0)]
        public string? MiddleName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(11)]
        public string IdentityNo { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string PasswordCheck { get; set; } = string.Empty;

        [Range(0, 2)]
        public int? GenderId { get; set; } = 0;

        [Required]
        public int CountryId { get; set; }

        [StringLength(100, MinimumLength = 0)]
        public string? EducationDegree { get; set; } = string.Empty;

        [Range(0, 3)]
        public int? UserRoleId { get; set; } = 0;

        public List<int>? LoanIds { get; set; } = [];

        public List<int>? PenaltIds { get; set; } = [];
    }
}
