using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.PublisherDTO;
using LibraryAPI.Entities.Enums;
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

        public Publisher DeleteEntity(Publisher publisher)
        {
            publisher.DeleteDateLog = DateTime.Now;
            publisher.State = State.Silindi;

            return publisher;
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

