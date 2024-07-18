using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Title : AbstractEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string TitleName { get; set; } = string.Empty;
    }
}
