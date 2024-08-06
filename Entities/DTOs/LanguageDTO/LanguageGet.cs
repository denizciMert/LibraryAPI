namespace LibraryAPI.Entities.DTOs.LanguageDTO
{
    public class LanguageGet
    {
        // Unique identifier for the language
        public int? Id { get; set; }

        // Name of the language
        public string? LanguageName { get; set; } = string.Empty;

        // Code associated with the language (e.g., 'EN' for English)
        public string? LanguageCode { get; set; } = string.Empty;

        // Date when the language record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the language record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the language record was deleted (if applicable)
        public DateTime? DeleteDateLog { get; set; }

        // Status of the language record (e.g., active, inactive)
        public string? State { get; set; }
    }
}