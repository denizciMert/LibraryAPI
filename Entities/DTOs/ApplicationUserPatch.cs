using LibraryAPI.Entities.Enums;

namespace LibraryAPI.Entities.DTOs
{
    public class ApplicationUserPatch
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? IdentityNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CountryId { get; set; }
        public Gender? Gender { get; set; }
    }
}
