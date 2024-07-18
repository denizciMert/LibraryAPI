using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class SubCategory : AbstractEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string SubCategoryName { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}
