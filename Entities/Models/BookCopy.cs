using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // BookCopy model class which extends AbstractEntity to include base entity properties
    public class BookCopy : AbstractEntity
    {
        // Foreign key for the Book entity, representing the book to which this copy belongs.
        public int BookId { get; set; }

        // Navigation property for the Book entity, linking to the Book based on BookId.
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }

        // Identifier for this particular copy of the book. It could be a serial number or any unique identifier.
        public int CopyNo { get; set; }

        // Indicates if this copy is reserved or not. Default value is set to false.
        public bool Reserved { get; set; } = false;
    }
}