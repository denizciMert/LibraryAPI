using LibraryAPI.Entities.DTOs.CityDTO; // Importing the DTOs for City
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between City entities and DTOs
    public class CityMapper
    {
        // Method to map CityPost DTO to City entity
        public City MapToEntity(CityPost dto)
        {
            var city = new City
            {
                CityName = dto.CityName,
                CountryId = dto.CountryId
            };

            return city;
        }

        // Method to map CityPost DTO to City entity with additional fields
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

        // Method to update an existing City entity with CityPost DTO data
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

        // Method to mark a City entity as deleted
        public City DeleteEntity(City city)
        {
            city.DeleteDateLog = DateTime.Now;
            city.State = State.Silindi;

            return city;
        }

        // Method to map City entity to CityGet DTO
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
