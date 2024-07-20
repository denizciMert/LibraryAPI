using LibraryAPI.Entities.DTOs.DistrictDTO;
using LibraryAPI.Entities.Enums;
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

        public District PostEntity(DistrictPost dto)
        {
            var district = new District
            {
                DistrictName = dto.District,
                CityId = dto.CityId,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return district;
        }

        public District UpdateEntity(District district, DistrictPost districtPost)
        {
            district.DistrictName = districtPost.District;
            district.CityId = districtPost.CityId;
            district.CreationDateLog = district.CreationDateLog;
            district.UpdateDateLog = DateTime.Now;
            district.DeleteDateLog = null;
            district.State = State.Güncellendi;

            return district;
        }

        public District DeleteEntity(District district)
        {
            district.DeleteDateLog = DateTime.Now;
            district.State = State.Silindi;

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

