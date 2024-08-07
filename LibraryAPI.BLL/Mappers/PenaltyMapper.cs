using LibraryAPI.Entities.DTOs.PenaltyDTO; // Importing the DTOs for Penalty
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Penalty entities and DTOs
    public class PenaltyMapper
    {
        // Method to map PenaltyPost DTO to Penalty entity
        public Penalty MapToEntity(PenaltyPost dto)
        {
            var penalty = new Penalty
            {
                PenaltyTypeId = dto.PenaltyTypeId,
                PenaltiedMembeId = dto.PenaltiedMemberId,
                LoanId = dto.LoanId
            };

            return penalty;
        }

        // Method to map PenaltyPost DTO to Penalty entity with additional fields
        public Penalty PostEntity(PenaltyPost dto)
        {
            var penalty = new Penalty
            {
                PenaltyTypeId = dto.PenaltyTypeId,
                PenaltiedMembeId = dto.PenaltiedMemberId,
                LoanId = dto.LoanId,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return penalty;
        }

        // Method to update an existing Penalty entity with PenaltyPost DTO data
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

        // Method to mark a Penalty entity as deleted
        public Penalty DeleteEntity(Penalty penalty)
        {
            penalty.DeleteDateLog = DateTime.Now;
            penalty.State = State.Silindi;

            return penalty;
        }

        // Method to map Penalty entity to PenaltyGet DTO
        public PenaltyGet MapToDto(Penalty entity)
        {
            var dto = new PenaltyGet
            {
                Id = entity.Id,
                MemberName =$"{entity.Member?.ApplicationUser?.FirstName} {entity.Member?.ApplicationUser?.MiddleName} {entity.Member?.ApplicationUser?.LastName}" ?? "",
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
