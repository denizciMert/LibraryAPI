using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.DTOs.EmployeeDTO
{
    public class EmployeePost
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
        [StringLength(320)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

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

        [Required]
        [Range(17002,int.MaxValue)]
        public int Salary { get; set; }

        [Required]
        [Range(0, 2)]
        public int GenderId { get; set; } = 0;

        [Required]
        public int CountryId { get; set; }

        [Required]
        [Range(0, 3)]
        public int? UserRoleId { get; set; } = 2;

        [Required]
        public int TitleId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ShiftId { get; set; }

        public string? UserImagePath { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? FileForm { get; set; }
    }
}
