using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    // Loan model class.
    // Represents a loan record in the library system, inheriting from AbstractEntity.
    public class Loan : AbstractEntity
    {
        // ID of the member who loaned the book.
        // The column type is nvarchar(450) to match the ApplicationUser ID type.
        [Column(TypeName = "nvarchar(450)")]
        public string LoanedMemberId { get; set; } = string.Empty;

        // Navigation property to the Member entity.
        // Represents the member associated with this loan.
        [ForeignKey(nameof(LoanedMemberId))]
        public Member? Member { get; set; }

        // ID of the employee who processed the loan.
        // The column type is nvarchar(450) to match the Employee ID type.
        [Column(TypeName = "nvarchar(450)")]
        public string EmployeeId { get; set; } = string.Empty;

        // Navigation property to the Employee entity.
        // Represents the employee who handled the loan.
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        // ID of the book being loaned.
        public int BookId { get; set; }

        // Navigation property to the Book entity.
        // Represents the book associated with this loan.
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }

        // Copy number of the book that was loaned.
        // Used to track which specific copy of the book was loaned.
        public short CopyNo { get; set; }

        // Date when the loan was made.
        public DateTime LoanDate { get; set; }

        // Due date for the return of the loaned book.
        public DateTime DueDate { get; set; }

        // Date when the book was actually returned.
        // Nullable to allow for cases where the book hasn't been returned yet.
        public DateTime? ReturnedDate { get; set; }

        // Indicates if the loan is currently active.
        // Used to track if the loan is still ongoing or if it has been completed or cancelled.
        public bool Active { get; set; }
    }
}
