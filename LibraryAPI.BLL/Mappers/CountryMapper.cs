using LibraryAPI.Entities.DTOs.CountryDTO; // Importing the DTOs for Country
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Country entities and DTOs
    public class CountryMapper
    {
        // Method to map CountryPost DTO to Country entity
        public Country MapToEntity(CountryPost dto)
        {
            var country = new Country
            {
                CountryName = dto.CountryName
            };

            return country;
        }

        // Method to map CountryPost DTO to Country entity with additional fields
        public Country PostEntity(CountryPost dto)
        {
            var country = new Country
            {
                CountryName = dto.CountryName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return country;
        }

        // Method to update an existing Country entity with CountryPost DTO data
        public Country UpdateEntity(Country country, CountryPost countryPost)
        {
            country.CountryName = countryPost.CountryName;
            country.CreationDateLog = country.CreationDateLog;
            country.UpdateDateLog = DateTime.Now;
            country.DeleteDateLog = null;
            country.State = State.Güncellendi;

            return country;
        }

        // Method to mark a Country entity as deleted
        public Country DeleteEntity(Country country)
        {
            country.DeleteDateLog = DateTime.Now;
            country.State = State.Silindi;

            return country;
        }

        // Method to map Country entity to CountryGet DTO
        public CountryGet MapToDto(Country entity)
        {
            var dto = new CountryGet
            {
                Id = entity.Id,
                CountryName = entity.CountryName,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
