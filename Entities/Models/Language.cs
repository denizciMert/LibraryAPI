using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Language model class.
    // Represents a language entity in the library system.
    public class Language : AbstractEntity
    {
        // Code for the language.
        // The column type is varchar(3) to store short language codes (e.g., 'ENG' for English).
        [Column(TypeName = "varchar(3)")]
        public string LanguageCode { get; set; } = string.Empty;

        // Name of the language.
        // The column type is varchar(100) to store the full name of the language (e.g., 'English').
        [Column(TypeName = "varchar(100)")]
        public string LanguageName { get; set; } = string.Empty;
    }
}