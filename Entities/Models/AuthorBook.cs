using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.Models
{
    public class AuthorBook
    {
        [Required]
        public int AuthorsId { get; set; }

        [ForeignKey(nameof(AuthorsId))]
        public Author? Author { get; set; }

        [Required]
        public int BooksId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}
