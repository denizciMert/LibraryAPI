using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.DTOs.EmployeeDTO
{
    public class EmployeePost
    {
        // First name of the employee
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        // Middle name of the employee (optional)
        [StringLength(50, MinimumLength = 0)]
        public string? MiddleName { get; set; } = string.Empty;

        // Last name of the employee
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        // Username for the employee
        [Required]
        public string UserName { get; set; } = string.Empty;

        // Email address of the employee
        [Required]
        [StringLength(320)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Phone number of the employee
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        // Identity number of the employee
        [Required]
        [StringLength(11)]
        public string IdentityNo { get; set; } = string.Empty;

        // Date of birth of the employee
        [Required]
        public DateTime DateOfBirth { get; set; }

        // Password for the employee account
        [Required]
        public string Password { get; set; } = string.Empty;

        // Confirmation of the password
        [Required]
        [Compare(nameof(Password))]
        public string PasswordCheck { get; set; } = string.Empty;

        // Salary of the employee
        [Required]
        [Range(17002, int.MaxValue)]
        public int Salary { get; set; }

        // Gender of the employee, represented by an ID
        [Required]
        [Range(0, 2)]
        public int GenderId { get; set; } = 0;

        // Country ID where the employee is located
        public int CountryId { get; set; }

        // Role ID assigned to the employee
        [Required]
        [Range(0, 3)]
        public int? UserRoleId { get; set; } = 2;

        // Title ID representing the job title of the employee
        [Required]
        public int TitleId { get; set; }

        // Department ID where the employee works
        [Required]
        public int DepartmentId { get; set; }

        // Shift ID representing the shift of the employee
        [Required]
        public int ShiftId { get; set; }

        // Path to the employee's image
        public string? UserImagePath { get; set; } = string.Empty;

        // Optional form file for uploading an image
        [NotMapped]
        public IFormFile? FileForm { get; set; }
    }
}
