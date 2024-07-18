using LibraryAPI.Entities.DTOs.CountryDTO;
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

