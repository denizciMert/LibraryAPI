using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // StudyTable model class, inheriting from AbstractEntity.
    // Represents a study table in the library system.
    public class StudyTable : AbstractEntity
    {
        // Code for identifying the study table, e.g., "T01", "T02", etc.
        // Column type is varchar with a maximum length of 6 characters.
        [Column(TypeName = "varchar(6)")]
        public string TableCode { get; set; } = string.Empty;

        // Indicates whether the table is currently reserved.
        // Defaults to false, meaning the table is not reserved.
        public bool Reserved { get; set; } = false;
    }
}