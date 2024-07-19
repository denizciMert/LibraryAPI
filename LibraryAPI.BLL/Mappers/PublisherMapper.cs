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

        public Category PostEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return category;
        }

        public Category UpdateEntity(Category category, CategoryPost categoryPost)
        {
            category.CategoryName = categoryPost.CategoryName;
            category.CreationDateLog = category.CreationDateLog;
            category.UpdateDateLog = DateTime.Now;
            category.DeleteDateLog = null;
            category.State = State.Güncellendi;

            return category;
        }

        public Category DeleteEntity(Category category)
        {
            category.DeleteDateLog = DateTime.Now;
            category.State = State.Silindi;

            return category;
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

