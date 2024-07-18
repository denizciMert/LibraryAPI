namespace LibraryAPI.Entities.DTOs.DepartmentDTO
{
    public class DepartmentGet
    {
        public int? Id { get; set; }
        public string? DepartmentName { get; set; } = string.Empty;
        public DateTime? CreatinDateLog { get; set; }
        public DateTime? UpdateDateLog { get; set; }
        public DateTime? DeleteDateLog { get; set; }
        public string? State { get; set; }
    }
}
