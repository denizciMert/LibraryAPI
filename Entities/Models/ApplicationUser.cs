using System.ComponentModel.DataAnnotations.Schema;
using LibraryAPI.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Entities.Models
{
    // ApplicationUser model class that extends IdentityUser to include additional properties.
    public class ApplicationUser : IdentityUser
    {
        // First name of the user, stored as a variable-length Unicode string with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string FirstName { get; set; } = string.Empty;

        // Middle name of the user, optional and stored as a variable-length Unicode string with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string? MiddleName { get; set; }

        // Last name of the user, stored as a variable-length Unicode string with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string LastName { get; set; } = string.Empty;

        // Identity number of the user, optional and stored as a variable-length Unicode string with a maximum length of 450 characters.
        [Column(TypeName = "nvarchar(450)")]
        public string? IdentityNo { get; set; } = string.Empty;

        // Date of birth of the user, optional.
        public DateTime? DateOfBirth { get; set; }

        // Date when the user registered, optional.
        public DateTime? DateOfRegister { get; set; }

        // List of addresses associated with the user.
        public List<Address>? Addresses { get; set; }

        // Foreign key for the country associated with the user.
        public int? CountryId { get; set; }

        // Navigation property for the related Country entity.
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }

        // Gender of the user, using the Gender enum.
        public Gender Gender { get; set; } = Gender.Belirtilmedi;

        // User role of the user, using the UserRole enum.
        public UserRole UserRole { get; set; } = UserRole.Ziyaretçi;

        // Indicates if the user is banned or not.
        public bool Banned { get; set; } = false;

        // State of the user entity, using the State enum.
        public State State { get; set; } = State.Eklendi;

        // Path to the user's image, optional and stored as a variable-length character string.
        [Column(TypeName = "varchar(450)")]
        public string? UserImagePath { get; set; } = string.Empty;

        // Date when the user record was last updated, optional.
        public DateTime? UpdateDateLog { get; set; }

        // Date when the user record was deleted, optional.
        public DateTime? DeleteDateLog { get; set; }
    }
}
