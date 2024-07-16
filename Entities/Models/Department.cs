using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Department : AbstractEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
