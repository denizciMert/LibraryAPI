using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Location : AbstractEntity
    {
        [Column(TypeName = "Varchar(6)")]
        public string ShelfCode { get; set; } = string.Empty;
    }
}
