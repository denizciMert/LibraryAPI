using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Entities.Models
{
    public class Loan : AbstractEntity
    {
        public string LoanedMemberId { get; set; } = string.Empty;

        [ForeignKey(nameof(LoanedMemberId))]
        public Member? Member { get; set; }
        
        public string EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }
        
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }

        public short CopyNo { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public bool Active { get; set; }
    }
}
