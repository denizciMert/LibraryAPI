using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class CityMapper
	{
        public City MapToEntity(CityPost dto)
        {
            var city = new City
            {
                CityName = dto.CityName,
                CountryId = dto.CountryId
            };

            return city;
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

        public CityGet MapToDto(City entity)
        {
            var dto = new CityGet
            {
                Id = entity.Id,
                CityName = entity.CityName,
                CountryName = entity.Country?.CountryName,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

