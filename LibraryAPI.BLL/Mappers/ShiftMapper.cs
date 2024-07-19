using System;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.ShiftDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class ShiftMapper
	{
        public Shift MapToEntity(ShiftPost shiftPost)
        {
            var entity = new Shift
            {
                ShiftType = shiftPost.ShiftType
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

        public ShiftGet MapToDto(Shift shift)
        {
            var dto = new ShiftGet
            {
                Id = shift.Id,
                ShiftType = shift.ShiftType,
                CreatinDateLog = shift.CreationDateLog,
                UpdateDateLog = shift.UpdateDateLog,
                DeleteDateLog = shift.DeleteDateLog,
                State = shift.State.ToString()
            };

            return dto;
        }
    }
}

