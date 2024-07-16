namespace LibraryAPI.Entities.DTOs.EmployeeDTO
{
    public class EmployeeGet
    {
        public string? Id { get; set; }
        public string? EmployeeName { get; set; } = string.Empty;
        public string? IdentityNo { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
        public float? Salary { get; set; }
        public string? Shift { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;
        public string? UserRole { get; set; } = string.Empty;
        public DateTime? DateOfRegister { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; } = string.Empty;
    }
}
