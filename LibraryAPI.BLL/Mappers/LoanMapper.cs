using System;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.LoanDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
    public class LoanMapper
    {
        public Loan MapToEntity(LoanPost dto)
        {
            var loan = new Loan
            {
                LoanedMemberId = dto.MemberId,
                EmployeeId = dto.EmployeeId, // Assuming EmployeeId is a string in Loan model
                BookId = dto.BookId,
                CopyNo = dto.CopyNo
                //LoanDate = DateTime.Now, // Varsayılan olarak şu anki zamanı atıyoruz
                //DueDate = DateTime.Now.AddDays(30), // Varsayılan olarak ödünç alma tarihine 30 gün ekliyoruz
                //Active = true, // Varsayılan olarak ödünç durumu aktif olarak işaretlenmiş
                //CreatedDate = DateTime.Now // Varsayılan olarak oluşturma tarihini şu anki zaman olarak atıyoruz
            };

            return loan;
        }

        public Category PostEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return category;
        }

        public Category UpdateEntity(Category category, CategoryPost categoryPost)
        {
            category.CategoryName = categoryPost.CategoryName;
            category.CreationDateLog = category.CreationDateLog;
            category.UpdateDateLog = DateTime.Now;
            category.DeleteDateLog = null;
            category.State = State.Güncellendi;

            return category;
        }

        public Category DeleteEntity(Category category)
        {
            category.DeleteDateLog = DateTime.Now;
            category.State = State.Silindi;

            return category;
        }

        public LoanGet MapToDto(Loan entity)
        {
            var dto = new LoanGet
            {
                Id = entity.Id,
                MemberName = $"{entity.Member.ApplicationUser.FirstName} {entity.Member.ApplicationUser.LastName}",
                MemberUserName = entity.Member?.ApplicationUser?.UserName,
                BookTitle = entity.Book?.BookTitle,
                BookIsbn = entity.Book?.Isbn,
                CopyNo = entity.CopyNo,
                EmployeeName = $"{entity.Employee.ApplicationUser.FirstName} {entity.Employee.ApplicationUser.LastName}",
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

