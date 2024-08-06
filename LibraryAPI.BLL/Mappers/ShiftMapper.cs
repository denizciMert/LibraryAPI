using LibraryAPI.Entities.DTOs.ShiftDTO; // Importing the DTOs for Shift
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Shift entities and DTOs
    public class ShiftMapper
    {
        // Method to map ShiftPost DTO to Shift entity
        public Shift MapToEntity(ShiftPost shiftPost)
        {
            var entity = new Shift
            {
                ShiftType = shiftPost.ShiftType
            };

            return entity;
        }

        // Method to map ShiftPost DTO to Shift entity with additional fields
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

        // Method to update an existing Shift entity with ShiftPost DTO data
        public Shift UpdateEntity(Shift shift, ShiftPost shiftPost)
        {
            shift.ShiftType = shiftPost.ShiftType;
            shift.CreationDateLog = shift.CreationDateLog;
            shift.UpdateDateLog = DateTime.Now;
            shift.DeleteDateLog = null;
            shift.State = State.Güncellendi;

            return shift;
        }

        // Method to mark a Shift entity as deleted
        public Shift DeleteEntity(Shift shift)
        {
            shift.DeleteDateLog = DateTime.Now;
            shift.State = State.Silindi;

            return shift;
        }

        // Method to map Shift entity to ShiftGet DTO
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
