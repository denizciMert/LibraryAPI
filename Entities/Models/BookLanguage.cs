using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.Models
{
    public class BookLanguage
    {
        [Required]
        public int LanguagesId { get; set; }

        [ForeignKey(nameof(LanguagesId))]
        public Language? Language { get; set; }

        [Required]
        public int BooksId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}
