using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.Enums;
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

        public City PostEntity(CityPost dto)
        {
            var city = new City
            {
                CityName = dto.CityName,
                CountryId = dto.CountryId,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return city;
        }

        public City UpdateEntity(City city, CityPost cityPost)
        {
            city.CityName = cityPost.CityName;
            city.CountryId = cityPost.CountryId;
            city.CreationDateLog = city.CreationDateLog;
            city.UpdateDateLog = DateTime.Now;
            city.DeleteDateLog = null;
            city.State = State.Güncellendi;

            return city;
        }

        public City DeleteEntity(City city)
        {
            city.DeleteDateLog = DateTime.Now;
            city.State = State.Silindi;

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

