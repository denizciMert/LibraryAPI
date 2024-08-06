namespace LibraryAPI.Entities.DTOs.LoanDTO
{
    public class LoanGet
    {
        // Unique identifier for the loan
        public int? Id { get; set; }

        // Name of the member who borrowed the book
        public string? MemberName { get; set; } = string.Empty;

        // Username of the member
        public string? MemberUserName { get; set; } = string.Empty;

        // Title of the borrowed book
        public string? BookTitle { get; set; } = string.Empty;

        // ISBN of the borrowed book
        public string? BookIsbn { get; set; } = string.Empty;

        // Copy number of the borrowed book
        public short? CopyNo { get; set; }

        // Name of the employee who processed the loan
        public string? EmployeeName { get; set; }

        // Username of the employee
        public string? EmployeeUserName { get; set; }

        // Date when the book was loaned
        public DateTime? LoanDate { get; set; }

        // Due date for returning the book
        public DateTime? DueDate { get; set; }

        // Date when the book was returned
        public DateTime? ReturnedDate { get; set; }

        // Indicates if the loan is still active
        public bool? Active { get; set; }

        // Date when the record was created
        public DateTime? CreatinDateLog { get; set; }

        // Date when the record was last updated
        public DateTime? UpdateDateLog { get; set; }

        // Date when the record was deleted
        public DateTime? DeleteDateLog { get; set; }

        // State or status of the loan record
        public string? State { get; set; } 
    }
}