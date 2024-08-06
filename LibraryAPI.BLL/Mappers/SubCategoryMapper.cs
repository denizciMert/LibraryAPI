using LibraryAPI.Entities.DTOs.SubCategoryDTO; // Importing DTOs related to SubCategory
using LibraryAPI.Entities.Enums; // Importing enums used in the project
using LibraryAPI.Entities.Models; // Importing entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between SubCategory entities and DTOs
    public class SubCategoryMapper
    {
        // Method to map SubCategoryPost DTO to SubCategory entity
        public SubCategory MapToEntity(SubCategoryPost subCategoryPost)
        {
            var entity = new SubCategory
            {
                SubCategoryName = subCategoryPost.SubCategoryName,
                CategoryId = subCategoryPost.CategoryId
            };

            return entity;
        }

        // Method to map SubCategoryPost DTO to SubCategory entity with additional fields
        public SubCategory PostEntity(SubCategoryPost subCategoryPost)
        {
            var subCategory = new SubCategory
            {
                SubCategoryName = subCategoryPost.SubCategoryName,
                CategoryId = subCategoryPost.CategoryId,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return subCategory;
        }

        // Method to update an existing SubCategory entity with SubCategoryPost DTO data
        public SubCategory UpdateEntity(SubCategory subCategory, SubCategoryPost subCategoryPost)
        {
            subCategory.SubCategoryName = subCategoryPost.SubCategoryName;
            subCategory.CategoryId = subCategoryPost.CategoryId;
            subCategory.CreationDateLog = subCategory.CreationDateLog;
            subCategory.UpdateDateLog = DateTime.Now;
            subCategory.DeleteDateLog = null;
            subCategory.State = State.Güncellendi;

            return subCategory;
        }

        // Method to mark a SubCategory entity as deleted
        public SubCategory DeleteEntity(SubCategory subCategory)
        {
            subCategory.DeleteDateLog = DateTime.Now;
            subCategory.State = State.Silindi;

            return subCategory;
        }

        // Method to map SubCategory entity to SubCategoryGet DTO
        public SubCategoryGet MapToDto(SubCategory subCategory)
        {
            var dto = new SubCategoryGet
            {
                Id = subCategory.Id,
                SubCategoryName = subCategory.SubCategoryName,
                CategoryName = subCategory.Category?.CategoryName,
                CreatinDateLog = subCategory.CreationDateLog,
                UpdateDateLog = subCategory.UpdateDateLog,
                DeleteDateLog = subCategory.DeleteDateLog,
                State = subCategory.State.ToString()
            };

            return dto;
        }
    }
}
