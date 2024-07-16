using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class StudyTable : AbstractEntity
    {
        [Column(TypeName = "varchar(6)")]
        public string TableCode { get; set; } = string.Empty;
    }
}
