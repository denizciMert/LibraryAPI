using LibraryAPI.Entities.DTOs.PublisherDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class PublisherMapper
	{
        public Publisher MapToEntity(PublisherPost dto)
        {
            var entity = new Publisher
            {
                PublisherName = dto.PublisherName,
                Phone = dto.Phone,
                EMail = dto.Email,
                ContactPerson = dto.ContactPerson
            };

            return entity;
        }

        public PublisherGet MapToDto(Publisher entity)
        {
            var dto = new PublisherGet
            {
                Id = entity.Id,
                PublisherName = entity.PublisherName,
                Phone = entity.Phone,
                Email = entity.EMail,
                ContactPerson = entity.ContactPerson,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}

