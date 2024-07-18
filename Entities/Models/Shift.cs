using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Shift : AbstractEntity
    {
        [Column(TypeName = "varchar(20)")]
        public string ShiftType { get; set; }
    }
}
