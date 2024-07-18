using System;
using LibraryAPI.Entities.DTOs.LocationDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
    public class LocationMapper
    {
        public Location MapToEntity(LocationPost dto)
        {
            var location = new Location
            {
                ShelfCode = dto.ShelfCode
            };

            return location;
        }

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

