using System;
using LibraryAPI.Entities.DTOs.ShiftDTO;
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

