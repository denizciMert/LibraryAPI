using System;
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
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

