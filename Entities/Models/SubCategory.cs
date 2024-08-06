using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // SubCategory model class, inheriting from AbstractEntity.
    // Represents a subcategory within a category in the library system.
    public class SubCategory : AbstractEntity
    {
        // Name of the subcategory, e.g., "Science Fiction", "Mystery", etc.
        // Column type is varchar with a maximum length of 100 characters.
        [Column(TypeName = "varchar(100)")]
        public string SubCategoryName { get; set; } = string.Empty;

        // Foreign key to the Category this subcategory belongs to.
        // Represents the ID of the parent category.
        public int CategoryId { get; set; }

        // Navigation property for the related Category entity.
        // Allows access to the Category object this subcategory is associated with.
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}