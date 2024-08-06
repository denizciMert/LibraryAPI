namespace LibraryAPI.Entities.DTOs.DepartmentDTO
{
    public class DepartmentGet
    {
        // Nullable int for optional department identifier.
        public int? Id { get; set; }

        // Department name, initialized to an empty string.
        public string? DepartmentName { get; set; } = string.Empty;

        // Optional date fields for tracking entity changes.
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }

        // State of the department (e.g., active, inactive).
        public string? State { get; set; }
    }
}