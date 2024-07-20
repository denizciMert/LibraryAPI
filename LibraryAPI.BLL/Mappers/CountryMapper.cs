using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.CountryDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class CountryMapper
	{
        public Country MapToEntity(CountryPost dto)
        {
            var country = new Country
            {
                CountryName = dto.CountryName
            };

            return country;
        }

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

        public Country UpdateEntity(Country country, CountryPost countryPost)
        {
            country.CountryName = country.CountryName;
            country.CreationDateLog = country.CreationDateLog;
            country.UpdateDateLog = DateTime.Now;
            country.DeleteDateLog = null;
            country.State = State.Güncellendi;

            return country;
        }

        public Country DeleteEntity(Country country)
        {
            country.DeleteDateLog = DateTime.Now;
            country.State = State.Silindi;

            return country;
        }

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

