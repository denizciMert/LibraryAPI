using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.Models
{
    public class BookSubCategory
    {
        [Required]
        public int SubCategoriesId { get; set; }

        [ForeignKey(nameof(SubCategoriesId))]
        public SubCategory? SubCategory { get; set; }

        [Required]
        public int BooksId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}
