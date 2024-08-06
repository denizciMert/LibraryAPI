using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Entities.Models
{
    // BookSubCategory model class that represents the relationship between books and subcategories.
    public class BookSubCategory
    {
        // Foreign key for the SubCategory entity, representing the subcategory that the book belongs to.
        [Required]
        public int SubCategoriesId { get; set; }

        // Navigation property for the SubCategory entity, linking to the SubCategory based on SubCategoriesId.
        [ForeignKey(nameof(SubCategoriesId))]
        public SubCategory? SubCategory { get; set; }

        // Foreign key for the Book entity, representing the book that is associated with the subcategory.
        [Required]
        public int BooksId { get; set; }

        // Navigation property for the Book entity, linking to the Book based on BooksId.
        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}