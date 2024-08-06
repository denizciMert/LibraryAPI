using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // BookRating model class that represents a rating given by a member to a book.
    public class BookRating
    {
        // Foreign key for the Book entity, representing the book that is rated.
        public int RatedBookId { get; set; }

        // Navigation property for the Book entity, linking to the Book based on RatedBookId.
        [ForeignKey(nameof(RatedBookId))]
        public Book? Book { get; set; }

        // Foreign key for the Member entity, representing the member who rated the book.
        [Column(TypeName = "nvarchar(450)")]
        public string RaterMemberId { get; set; } = string.Empty;

        // Navigation property for the Member entity, linking to the Member based on RaterMemberId.
        [ForeignKey(nameof(RaterMemberId))]
        public Member? Member { get; set; }

        // Rating value given by the member to the book. This can be a float to accommodate fractional ratings.
        public float Rate { get; set; }
    }
}