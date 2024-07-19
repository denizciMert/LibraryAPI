using System;
using LibraryAPI.Entities.DTOs.CategoryDTO;
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

