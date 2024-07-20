using System;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.LocationDTO;
using LibraryAPI.Entities.Enums;
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

        public Location UpdateEntity(Location location, LocationPost locationPost)
        {
            location.ShelfCode = locationPost.ShelfCode;
            location.CreationDateLog = location.CreationDateLog;
            location.UpdateDateLog = DateTime.Now;
            location.DeleteDateLog = null;
            location.State = State.Güncellendi;

            return location;
        }

        public Location DeleteEntity(Location location)
        {
            location.DeleteDateLog = DateTime.Now;
            location.State = State.Silindi;

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

