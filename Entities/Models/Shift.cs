using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Shift model class, inheriting from AbstractEntity.
    // Represents a work shift in the library system.
    public class Shift : AbstractEntity
    {
        // The type or name of the shift, e.g., "Morning", "Afternoon", etc.
        // Column type is varchar with a maximum length of 20 characters.
        [Column(TypeName = "varchar(20)")]
        public string ShiftType { get; set; } = string.Empty;
    }
}