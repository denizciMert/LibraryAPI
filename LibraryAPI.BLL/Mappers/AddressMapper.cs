using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Enums;
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

        public Address PostEntity(AddressPost dto)
        {
            var address = new Address
            {
                AddressString = dto.AddressString,
                DistrictId = dto.DistrictId,
                UserId = dto.UserId,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return address;
        }

        public Address UpdateEntity(Address address, AddressPost addressPost)
        {
            address.AddressString = addressPost.AddressString;
            address.DistrictId = addressPost.DistrictId;
            address.UserId = addressPost.UserId;
            address.CreationDateLog = address.CreationDateLog;
            address.UpdateDateLog = DateTime.Now;
            address.DeleteDateLog = null;
            address.State = State.Güncellendi;

            return address;
        }

        public Address DeleteEntity(Address address)
        {
            address.DeleteDateLog = DateTime.Now;
            address.State = State.Silindi;

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

