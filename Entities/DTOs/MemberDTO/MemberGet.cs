namespace LibraryAPI.Entities.DTOs.MemberDTO
{
    public class MemberGet
    {
        public string? Id { get; set; }
        public string? MemberName { get; set; } = string.Empty;
        public string? IdentityNo { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? EducationDegree { get; set; } = string.Empty;
        public List<string>? Adresses { get; set; } = [];
        public List<string>? Loans { get; set; } = [];
        public List<string>? Penalties { get; set; } = [];
        public string? ImagePath { get; set; } = string.Empty;
        public string? UserRole { get; set; } = string.Empty;
        public DateTime? DateOfRegister { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; } = string.Empty;
    }
}
