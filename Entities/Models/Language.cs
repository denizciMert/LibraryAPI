using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Language : AbstractEntity
    {
        [Column(TypeName = "Char(3)")]
        public string LanguageCode { get; set; } = string.Empty;

        [Column(TypeName = "Varchar(100)")]
        public string LanguageName { get; set; } = string.Empty;
    }
}
