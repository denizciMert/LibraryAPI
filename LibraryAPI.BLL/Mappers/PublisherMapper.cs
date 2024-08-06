using LibraryAPI.Entities.DTOs.PublisherDTO; // Importing the DTOs for Publisher
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Publisher entities and DTOs
    public class PublisherMapper
    {
        // Method to map PublisherPost DTO to Publisher entity
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

        // Method to map PublisherPost DTO to Publisher entity with additional fields
        public Publisher PostEntity(PublisherPost dto)
        {
            var publisher = new Publisher
            {
                PublisherName = dto.PublisherName,
                Phone = dto.Phone,
                EMail = dto.Email,
                ContactPerson = dto.ContactPerson,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return publisher;
        }

        // Method to update an existing Publisher entity with PublisherPost DTO data
        public Publisher UpdateEntity(Publisher publisher, PublisherPost publisherPost)
        {
            publisher.PublisherName = publisherPost.PublisherName;
            publisher.Phone = publisherPost.Phone;
            publisher.EMail = publisherPost.Email;
            publisher.ContactPerson = publisherPost.ContactPerson;
            publisher.CreationDateLog = publisher.CreationDateLog;
            publisher.UpdateDateLog = DateTime.Now;
            publisher.DeleteDateLog = null;
            publisher.State = State.Güncellendi;

            return publisher;
        }

        // Method to mark a Publisher entity as deleted
        public Publisher DeleteEntity(Publisher publisher)
        {
            publisher.DeleteDateLog = DateTime.Now;
            publisher.State = State.Silindi;

            return publisher;
        }

        // Method to map Publisher entity to PublisherGet DTO
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
