using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Employee model class.
    // Represents an employee entity in the library system.
    public class Employee
    {
        // Primary key for the Employee entity.
        // The Id is of type string and is expected to be unique across the system.
        [Key]
        [Column(TypeName = "nvarchar(450)")]
        public string Id { get; set; } = string.Empty;

        // Foreign key property linking to the ApplicationUser entity.
        // Represents the application user associated with this employee.
        [ForeignKey(nameof(Id))]
        public ApplicationUser? ApplicationUser { get; set; }

        // Optional salary property for the employee.
        // Default value is set to 17002.
        public float? Salary { get; set; } = 17002;

        // Optional TitleId property linking to the Title entity.
        // Represents the title or job position of the employee.
        public int? TitleId { get; set; } = 0;

        // Navigation property to the Title entity.
        public Title? Title { get; set; }

        // Optional DepartmentId property linking to the Department entity.
        // Represents the department in which the employee works.
        public int? DepartmentId { get; set; } = 0;

        // Navigation property to the Department entity.
        public Department? Department { get; set; }

        // Optional ShiftId property linking to the Shift entity.
        // Represents the shift schedule of the employee.
        public int? ShiftId { get; set; } = 0;

        // Navigation property to the Shift entity.
        public Shift? Shift { get; set; }
    }
}