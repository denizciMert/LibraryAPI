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

        public Shift PostEntity(ShiftPost shiftPost)
        {
            var shift = new Shift
            {
                ShiftType = shiftPost.ShiftType,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return shift;
        }

        public Shift UpdateEntity(Shift shift, ShiftPost shiftPost)
        {
            shift.ShiftType = shiftPost.ShiftType;
            shift.CreationDateLog = shift.CreationDateLog;
            shift.UpdateDateLog = DateTime.Now;
            shift.DeleteDateLog = null;
            shift.State = State.Güncellendi;

            return shift;
        }

        public Shift DeleteEntity(Shift shift)
        {
            shift.DeleteDateLog = DateTime.Now;
            shift.State = State.Silindi;

            return shift;
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

