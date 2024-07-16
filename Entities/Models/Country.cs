using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Country : AbstractEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string CountryName { get; set; } = string.Empty;
    }
}
