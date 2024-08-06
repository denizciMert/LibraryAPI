using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Department model class that extends the AbstractEntity class.
    // Represents a department entity within the library system.
    public class Department : AbstractEntity
    {
        // Column attribute specifies the data type and length for the DepartmentName property.
        [Column(TypeName = "varchar(100)")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}