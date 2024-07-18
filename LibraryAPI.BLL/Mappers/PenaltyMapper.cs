using LibraryAPI.Entities.DTOs.PenaltyDTO;
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

