using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.PenaltyDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class PenaltyMapper
	{
        public Penalty MapToEntity(PenaltyPost dto)
        {
            var penalty = new Penalty
            {
                PenaltyTypeId = dto.PenaltyId,
                PenaltiedMembeId = dto.PenaltiedMemberId,
                LoanId = dto.LoanId
            };

            return penalty;
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

        public PenaltyGet MapToDto(Penalty entity)
        {
            var dto = new PenaltyGet
            {
                Id = entity.Id,
                MemberName = entity.Member?.ApplicationUser?.FirstName ?? "",
                UserName = entity.Member?.ApplicationUser?.UserName ?? "",
                BookTitle = entity.Loan?.Book?.BookTitle ?? "",
                Isbn = entity.Loan?.Book?.Isbn ?? "",
                CopyNo = entity.Loan?.CopyNo ?? 0,
                PenaltyType = entity.PenaltyType?.PenaltyName ?? "",
                PenaltyAmount = entity.PenaltyType?.AmountToPay ?? 0,
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

