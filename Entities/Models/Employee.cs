using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Employee
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [ForeignKey(nameof(Id))]
        public ApplicationUser? ApplicationUser { get; set; }
        public float? Salary { get; set; } = 17002;

        public int? TitleId { get; set; } = 0;

        public Title? Title { get; set; }

        public int? DepartmentId { get; set; } = 0;

        public Department? Department { get; set; }

        public int? ShiftId { get; set; } = 0;

        public Shift? Shift { get; set; }
    }
}
