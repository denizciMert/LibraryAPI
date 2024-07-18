using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class AddressMapper
	{
        public Address MapToEntity(AddressPost dto)
        {
            var address = new Address
            {
                AddressString = dto.AddressString,
                DistrictId = dto.DistrictId,
                UserId = dto.UserId
            };

            return address;
        }

        public AddressGet MapToDto(Address entity)
        {
            var dto = new AddressGet
            {
                Id = entity.Id,
                AddressString = entity.AddressString,
                District = entity.District?.DistrictName,
                City = entity.District?.City.CityName,
                Country = entity.District?.City.Country.CountryName,
                UserName = entity.ApplicationUser?.UserName,
                UserFullName = $"{entity.ApplicationUser?.FirstName} {entity.ApplicationUser?.LastName}",
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

