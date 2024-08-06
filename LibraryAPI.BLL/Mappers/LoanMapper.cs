using LibraryAPI.Entities.DTOs.LoanDTO; // Importing the DTOs for Loan
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Loan entities and DTOs
    public class LoanMapper
    {
        // Method to map LoanPost DTO to Loan entity
        public Loan MapToEntity(LoanPost dto)
        {
            var loan = new Loan
            {
                LoanedMemberId = dto.MemberId,
                EmployeeId = dto.EmployeeId,
                BookId = dto.BookId,
                CopyNo = dto.CopyNo
            };

            return loan;
        }

        // Method to map LoanPost DTO to Loan entity with additional fields
        public Loan PostEntity(LoanPost dto)
        {
            var loan = new Loan
            {
                LoanedMemberId = dto.MemberId,
                EmployeeId = dto.EmployeeId,
                BookId = dto.BookId,
                CopyNo = dto.CopyNo,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(31),
                Active = true,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return loan;
        }

        // Method to update an existing Loan entity with LoanPost DTO data
        public Loan UpdateEntity(Loan loan, LoanPost loanPost)
        {
            loan.LoanedMemberId = loanPost.MemberId;
            loan.EmployeeId = loanPost.EmployeeId;
            loan.BookId = loanPost.BookId;
            loan.CopyNo = loanPost.CopyNo;
            loan.LoanDate = loan.LoanDate;
            loan.DueDate = loan.DueDate;
            loan.Active = loan.Active;
            loan.CreationDateLog = loan.CreationDateLog;
            loan.UpdateDateLog = DateTime.Now;
            loan.DeleteDateLog = null;
            loan.State = State.Güncellendi;

            return loan;
        }

        // Method to mark a Loan entity as deleted
        public Loan DeleteEntity(Loan loan)
        {
            loan.Active = false;
            loan.DeleteDateLog = DateTime.Now;
            loan.State = State.Silindi;

            return loan;
        }

        // Method to map Loan entity to LoanGet DTO
        public LoanGet MapToDto(Loan entity)
        {
            var dto = new LoanGet
            {
                Id = entity.Id,
                MemberName = $"{entity.Member!.ApplicationUser!.FirstName} {entity.Member!.ApplicationUser!.MiddleName} {entity.Member.ApplicationUser.LastName}",
                MemberUserName = entity.Member?.ApplicationUser?.UserName,
                BookTitle = entity.Book?.BookTitle,
                BookIsbn = entity.Book?.Isbn,
                CopyNo = entity.CopyNo,
                EmployeeName = $"{entity.Employee!.ApplicationUser!.FirstName} {entity.Employee.ApplicationUser.LastName}",
                EmployeeUserName = entity.Employee.ApplicationUser.UserName,
                LoanDate = entity.LoanDate,
                DueDate = entity.DueDate,
                ReturnedDate = entity.ReturnedDate,
                Active = entity.Active,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
