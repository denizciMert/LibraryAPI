using LibraryAPI.Entities.DTOs.SubCategoryDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class SubCategoryMapper
	{
        public SubCategory MapToEntity(SubCategoryPost subCategoryPost)
        {
            var entity = new SubCategory
            {
                SubCategoryName = subCategoryPost.SubCategoryName,
                CategoryId = subCategoryPost.CategoryId
            };

            return entity;
        }

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

        public SubCategory DeleteEntity(SubCategory subCategory)
        {
            subCategory.DeleteDateLog = DateTime.Now;
            subCategory.State = State.Silindi;

            return subCategory;
        }

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

