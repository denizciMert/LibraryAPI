namespace LibraryAPI.Entities.DTOs.EmployeeDTO
{
    public class EmployeeGet
    {
        // Unique identifier for the employee
        public string? Id { get; set; }

        // Name of the employee
        public string? EmployeeName { get; set; } = string.Empty;

        // Identity number of the employee
        public string? IdentityNo { get; set; } = string.Empty;

        // Username associated with the employee
        public string? UserName { get; set; } = string.Empty;

        // Email address of the employee
        public string? Email { get; set; } = string.Empty;

        // Phone number of the employee
        public string? Phone { get; set; } = string.Empty;

        // Date of birth of the employee
        public DateTime? DateOfBirth { get; set; }

        // Gender of the employee
        public string? Gender { get; set; } = string.Empty;

        // List of addresses associated with the employee
        public List<string>? Addresses { get; set; }

        // Country where the employee is located
        public string? Country { get; set; } = string.Empty;

        // Job title of the employee
        public string? Title { get; set; } = string.Empty;

        // Department where the employee works
        public string? Department { get; set; } = string.Empty;

        // Salary of the employee
        public float? Salary { get; set; }

        // Shift information for the employee
        public string? Shift { get; set; } = string.Empty;

        // Indicates whether the employee is banned
        public bool Banned { get; set; }

        // Path to the employee's image
        public string? ImagePath { get; set; } = string.Empty;

        // Role assigned to the employee
        public string? UserRole { get; set; } = string.Empty;

        // Date when the employee was registered
        public DateTime? DateOfRegister { get; set; }

        // Date when the employee information was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the employee information was deleted
        public DateTime? DeleteDateLog { get; set; }

        // Current state of the employee record
        public string? State { get; set; } = string.Empty;
    }
}
