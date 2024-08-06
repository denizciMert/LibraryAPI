using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryAPI.Entities.DTOs.MemberDTO
{
    public class MemberPost
    {
        // First name of the member
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        // Middle name of the member (optional)
        [StringLength(50, MinimumLength = 0)]
        public string? MiddleName { get; set; } = string.Empty;

        // Last name of the member
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        // Username of the member
        [Required]
        public string UserName { get; set; } = string.Empty;

        // Email address of the member
        [Required]
        [StringLength(320)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Identity number of the member
        [Required]
        [StringLength(11)]
        public string IdentityNo { get; set; } = string.Empty;

        // Phone number of the member
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        // Date of birth of the member
        [Required]
        public DateTime DateOfBirth { get; set; }

        // Password for the member
        [Required]
        public string Password { get; set; } = string.Empty;

        // Password confirmation for validation
        [Required]
        [Compare(nameof(Password))]
        public string PasswordCheck { get; set; } = string.Empty;

        // Gender of the member (optional)
        [Range(0, 3)]
        public int? GenderId { get; set; } = 0;

        // Country identifier of the member
        public int CountryId { get; set; }

        // Education degree of the member (optional)
        [StringLength(100, MinimumLength = 0)]
        public string? EducationDegree { get; set; } = string.Empty;

        // User role identifier (Swagger documentation ignores this property)
        [SwaggerIgnore]
        [Range(0, 3)]
        public int? UserRoleId { get; set; } = 0;

        // Path to the member's user image (Swagger documentation ignores this property)
        [SwaggerIgnore]
        public string? UserImagePath { get; set; } = string.Empty;

        // File form for user image upload
        [NotMapped]
        public IFormFile? FileForm { get; set; }
    }
}
