using LibraryAPI.Entities.Enums;

namespace LibraryAPI.Entities.DTOs
{
    public class ApplicationUserPatch
    {
        // First name of the user
        public string? FirstName { get; set; }

        // Middle name of the user
        public string? MiddleName { get; set; }

        // Last name of the user
        public string? LastName { get; set; }

        // Identity number of the user
        public string? IdentityNo { get; set; }

        // Date of birth of the user
        public DateTime? DateOfBirth { get; set; }

        // Country ID associated with the user
        public int? CountryId { get; set; }

        // Gender of the user
        public Gender? Gender { get; set; }
    }
}