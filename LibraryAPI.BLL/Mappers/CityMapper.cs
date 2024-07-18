using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class CityMapper
	{
        public City MapToEntity(CityPost dto)
        {
            var city = new City
            {
                CityName = dto.CityName,
                CountryId = dto.CountryId
            };

            return city;
        }

        public CityGet MapToDto(City entity)
        {
            var dto = new CityGet
            {
                Id = entity.Id,
                CityName = entity.CityName,
                CountryName = entity.Country?.CountryName,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

