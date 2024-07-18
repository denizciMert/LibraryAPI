using System;
using LibraryAPI.Entities.DTOs.DistrictDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class DistrictMapper
	{
        public District MapToEntity(DistrictPost dto)
        {
            var district = new District
            {
                DistrictName = dto.District,
                CityId = dto.CityId
            };

            return district;
        }

        public DistrictGet MapToDto(District entity)
        {
            var dto = new DistrictGet
            {
                Id = entity.Id,
                District = entity.DistrictName,
                City = entity.City?.CityName ?? string.Empty,
                Country = entity.City?.Country?.CountryName ?? string.Empty,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

