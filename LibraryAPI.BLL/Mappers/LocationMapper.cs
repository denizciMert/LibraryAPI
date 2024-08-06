using LibraryAPI.Entities.DTOs.LocationDTO; // Importing the DTOs for Location
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Location entities and DTOs
    public class LocationMapper
    {
        // Method to map LocationPost DTO to Location entity
        public Location MapToEntity(LocationPost dto)
        {
            var location = new Location
            {
                ShelfCode = dto.ShelfCode
            };

            return location;
        }

        // Method to map LocationPost DTO to Location entity with additional fields
        public Location PostEntity(LocationPost dto)
        {
            var location = new Location
            {
                ShelfCode = dto.ShelfCode,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return location;
        }

        // Method to update an existing Location entity with LocationPost DTO data
        public Location UpdateEntity(Location location, LocationPost locationPost)
        {
            location.ShelfCode = locationPost.ShelfCode;
            location.CreationDateLog = location.CreationDateLog;
            location.UpdateDateLog = DateTime.Now;
            location.DeleteDateLog = null;
            location.State = State.Güncellendi;

            return location;
        }

        // Method to mark a Location entity as deleted
        public Location DeleteEntity(Location location)
        {
            location.DeleteDateLog = DateTime.Now;
            location.State = State.Silindi;

            return location;
        }

        // Method to map Location entity to LocationGet DTO
        public LocationGet MapToDto(Location entity)
        {
            var dto = new LocationGet
            {
                Id = entity.Id,
                ShelfCode = entity.ShelfCode,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
