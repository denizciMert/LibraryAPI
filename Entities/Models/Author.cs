using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Author : AbstractEntity
    {
        [Column(TypeName = "Nvarchar(1000)")]
        public string AuthorFullName { get; set; } = string.Empty;

        [Column(TypeName = "Nvarchar(1000)")]
        public string? Biography { get; set; }

        public short DateOfBirth { get; set; }

        public short? DateOfDeath { get; set; }
    }
}
