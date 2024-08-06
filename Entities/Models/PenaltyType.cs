using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // PenaltyType model class, inheriting from AbstractEntity.
    // Represents a type of penalty that can be applied to members.
    public class PenaltyType : AbstractEntity
    {
        // Name of the penalty type (e.g., "Late Fee", "Lost Item").
        // Column type is varchar with a maximum length of 50 characters.
        [Column(TypeName = "varchar(50)")]
        public string? PenaltyName { get; set; } = string.Empty;

        // Amount to be paid for this type of penalty.
        // Represents the monetary value associated with the penalty.
        public float AmountToPay { get; set; }
    }
}