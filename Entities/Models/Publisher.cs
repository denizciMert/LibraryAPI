using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Publisher : AbstractEntity
    {
        public string PublisherName { get; set; } = string.Empty;

        [Column(TypeName = "Varchar(15)")]
        public string? Phone { get; set; }

        [Column(TypeName = "Varchar(320)")]
        public string? EMail { get; set; }

        public string? ContactPerson { get; set; }
    }
}
