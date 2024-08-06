using LibraryAPI.Entities.DTOs.CategoryDTO; // Importing the DTOs for Category
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Category entities and DTOs
    public class CategoryMapper
    {
        // Method to map CategoryPost DTO to Category entity
        public Category MapToEntity(CategoryPost dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            return category;
        }

        // Method to map CategoryPost DTO to Category entity with additional fields
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

        // Method to update an existing Category entity with CategoryPost DTO data
        public Category UpdateEntity(Category category, CategoryPost categoryPost)
        {
            category.CategoryName = categoryPost.CategoryName;
            category.CreationDateLog = category.CreationDateLog;
            category.UpdateDateLog = DateTime.Now;
            category.DeleteDateLog = null;
            category.State = State.Güncellendi;

            return category;
        }

        // Method to mark a Category entity as deleted
        public Category DeleteEntity(Category category)
        {
            category.DeleteDateLog = DateTime.Now;
            category.State = State.Silindi;

            return category;
        }

        // Method to map Category entity to CategoryGet DTO
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
