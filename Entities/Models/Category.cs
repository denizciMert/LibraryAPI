using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Category : AbstractEntity
    {
        [Column(TypeName = "Varchar(100)")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
