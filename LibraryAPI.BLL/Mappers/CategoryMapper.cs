﻿using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Enums;
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

