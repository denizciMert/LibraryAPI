using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class CategoryMapper
	{
        public Category MapToEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            return category;
        }

        public CategoryGet MapToDto(Category entity)
        {
            var dto = new CategoryGet
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
}
}

