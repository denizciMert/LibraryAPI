using LibraryAPI.Entities.DTOs.DistrictDTO; // Importing the DTOs for District
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between District entities and DTOs
    public class DistrictMapper
    {
        // Method to map DistrictPost DTO to District entity
        public District MapToEntity(DistrictPost dto)
        {
            var district = new District
            {
                DistrictName = dto.District,
                CityId = dto.CityId
            };

            return district;
        }

        // Method to map DistrictPost DTO to District entity with additional fields
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

        // Method to update an existing District entity with DistrictPost DTO data
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

        // Method to mark a District entity as deleted
        public District DeleteEntity(District district)
        {
            district.DeleteDateLog = DateTime.Now;
            district.State = State.Silindi;

            return district;
        }

        // Method to map District entity to DistrictGet DTO
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
