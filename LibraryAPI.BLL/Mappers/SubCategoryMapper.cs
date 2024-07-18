using System;
using LibraryAPI.Entities.DTOs.SubCategoryDTO;
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

