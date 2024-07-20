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

        public Penalty PostEntity(PenaltyPost dto)
        {
            var penalty = new Penalty
            {
                PenaltyTypeId = dto.PenaltyId,
                PenaltiedMembeId = dto.PenaltiedMemberId,
                LoanId = dto.LoanId,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return penalty;
        }

        public Penalty UpdateEntity(Penalty penalty, PenaltyPost penaltyPost)
        {
            penalty.PenaltyTypeId = penaltyPost.PenaltyTypeId;
            penalty.LoanId = penaltyPost.LoanId;
            penalty.PenaltiedMembeId = penaltyPost.PenaltiedMemberId;
            penalty.CreationDateLog = penalty.CreationDateLog;
            penalty.UpdateDateLog = DateTime.Now;
            penalty.DeleteDateLog = null;
            penalty.State = State.Güncellendi;

            return penalty;
        }

        public Penalty DeleteEntity(Penalty penalty)
        {
            penalty.DeleteDateLog = DateTime.Now;
            penalty.State = State.Silindi;

            return penalty;
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

