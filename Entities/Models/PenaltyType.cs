using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class PenaltyType : AbstractEntity
    {
        [Column(TypeName = "varchar(50)")]
        public string PenaltyName { get; set; } = string.Empty;

        public float AmountToPay { get; set; }
    }
}
