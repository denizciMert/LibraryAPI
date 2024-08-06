using LibraryAPI.Entities.DTOs.PenaltyTypeDTO; // Importing the DTOs for PenaltyType
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between PenaltyType entities and DTOs
    public class PenaltyTypeMapper
    {
        // Method to map PenaltyTypePost DTO to PenaltyType entity
        public PenaltyType MapToEntity(PenaltyTypePost dto)
        {
            var entity = new PenaltyType
            {
                PenaltyName = dto.PenaltyType,
                AmountToPay = dto.AmountToPay
            };

            return entity;
        }

        // Method to map PenaltyTypePost DTO to PenaltyType entity with additional fields
        public PenaltyType PostEntity(PenaltyTypePost dto)
        {
            var penalty = new PenaltyType
            {
                PenaltyName = dto.PenaltyType,
                AmountToPay = dto.AmountToPay,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return penalty;
        }

        // Method to update an existing PenaltyType entity with PenaltyTypePost DTO data
        public PenaltyType UpdateEntity(PenaltyType penalty, PenaltyTypePost penaltyTypePost)
        {
            penalty.PenaltyName = penaltyTypePost.PenaltyType;
            penalty.AmountToPay = penaltyTypePost.AmountToPay;
            penalty.CreationDateLog = penalty.CreationDateLog;
            penalty.UpdateDateLog = DateTime.Now;
            penalty.DeleteDateLog = null;
            penalty.State = State.Güncellendi;

            return penalty;
        }

        // Method to mark a PenaltyType entity as deleted
        public PenaltyType DeleteEntity(PenaltyType penaltyType)
        {
            penaltyType.DeleteDateLog = DateTime.Now;
            penaltyType.State = State.Silindi;

            return penaltyType;
        }

        // Method to map PenaltyType entity to PenaltyTypeGet DTO
        public PenaltyTypeGet MapToDto(PenaltyType entity)
        {
            var dto = new PenaltyTypeGet
            {
                Id = entity.Id,
                PenaltyName = entity.PenaltyName,
                AmountToPay = entity.AmountToPay,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
