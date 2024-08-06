using LibraryAPI.Entities.DTOs.AddressDTO; // Importing the DTOs for Address
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Address entities and DTOs
    public class AddressMapper
    {
        // Method to map AddressPost DTO to Address entity
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

        // Method to map AddressPost DTO to Address entity with additional fields
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

        // Method to update an existing Address entity with AddressPost DTO data
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

        // Method to mark an Address entity as deleted
        public Address DeleteEntity(Address address)
        {
            address.DeleteDateLog = DateTime.Now;
            address.State = State.Silindi;

            return address;
        }

        // Method to map Address entity to AddressGet DTO
        public AddressGet MapToDto(Address entity)
        {
            var dto = new AddressGet
            {
                Id = entity.Id,
                AddressString = entity.AddressString,
                District = entity.District?.DistrictName,
                City = entity.District?.City!.CityName,
                Country = entity.District?.City!.Country!.CountryName,
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
