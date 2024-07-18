using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class BookCopy
    {
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }
        public int CopyNo { get; set; }
        public bool Reserved { get; set; } = false;
    }
}
