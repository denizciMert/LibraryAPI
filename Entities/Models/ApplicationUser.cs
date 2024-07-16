using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryAPI.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string? IdentityNo { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfRegister { get; set; }

        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }

        public Gender Gender { get; set; } = Gender.Belirtilmedi;

        public UserRole UserRole { get; set; } = UserRole.Ziyaretçi;

        public bool Banned { get; set; } = false;

        public State State { get; set; } = State.Eklendi;

        public string? UserImagePath { get; set; } = string.Empty;

        public DateTime? UpdateDateLog { get; set; }

        public DateTime? DeleteDateLog { get; set; }
    }
}
