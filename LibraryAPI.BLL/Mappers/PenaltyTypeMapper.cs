using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class PenaltyTypeMapper
	{
        public PenaltyType MapToEntity(PenaltyTypePost dto)
        {
            var entity = new PenaltyType
            {
                PenaltyName = dto.PenaltyType,
                AmountToPay = dto.AmountToPay
            };

            return entity;
        }

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

        public PenaltyType DeleteEntity(PenaltyType penaltyType)
        {
            penaltyType.DeleteDateLog = DateTime.Now;
            penaltyType.State = State.Silindi;

            return penaltyType;
        }

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

