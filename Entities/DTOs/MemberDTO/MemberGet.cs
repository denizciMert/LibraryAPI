namespace LibraryAPI.Entities.DTOs.MemberDTO
{
    public class MemberGet
    {
        // Unique identifier for the member
        public string? Id { get; set; }

        // Full name of the member
        public string? MemberName { get; set; } = string.Empty;

        // Identity number of the member
        public string? IdentityNo { get; set; } = string.Empty;

        // Username of the member
        public string? UserName { get; set; } = string.Empty;

        // Email address of the member
        public string? Email { get; set; } = string.Empty;

        // Phone number of the member
        public string? Phone { get; set; } = string.Empty;

        // Date of birth of the member
        public DateTime? DateOfBirth { get; set; }

        // Gender of the member
        public string? Gender { get; set; } = string.Empty;

        // Country of the member
        public string? Country { get; set; } = string.Empty;

        // Education degree of the member
        public string? EducationDegree { get; set; } = string.Empty;

        // Addresses of the member
        public List<string>? Adresses { get; set; } = [];

        // Loans associated with the member
        public List<string>? Loans { get; set; } = [];

        // Penalties associated with the member
        public List<string>? Penalties { get; set; } = [];

        // Whether the member is banned or not
        public bool Banned { get; set; }

        // Path to the member's image
        public string? ImagePath { get; set; } = string.Empty;

        // Role of the user
        public string? UserRole { get; set; } = string.Empty;

        // Date when the member registered
        public DateTime? DateOfRegister { get; set; }

        // Date when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State of the record (e.g., active, inactive)
        public string? State { get; set; } = string.Empty;
    }
}
