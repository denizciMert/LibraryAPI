using LibraryAPI.Entities.DTOs.DepartmentDTO; // Importing the DTOs for Department
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Mappers
{
    // Mapper class for converting between Department entities and DTOs
    public class DepartmentMapper
    {
        // Method to map DepartmentPost DTO to Department entity
        public Department MapToEntity(DepartmentPost dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName
            };

            return department;
        }

        // Method to map DepartmentPost DTO to Department entity with additional fields
        public Department PostEntity(DepartmentPost dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                CreationDateLog = DateTime.Now,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return department;
        }

        // Method to update an existing Department entity with DepartmentPost DTO data
        public Department UpdateEntity(Department department, DepartmentPost departmentPost)
        {
            department.DepartmentName = departmentPost.DepartmentName;
            department.CreationDateLog = department.CreationDateLog;
            department.UpdateDateLog = DateTime.Now;
            department.DeleteDateLog = null;
            department.State = State.Güncellendi;

            return department;
        }

        // Method to mark a Department entity as deleted
        public Department DeleteEntity(Department department)
        {
            department.DeleteDateLog = DateTime.Now;
            department.State = State.Silindi;

            return department;
        }

        // Method to map Department entity to DepartmentGet DTO
        public DepartmentGet MapToDto(Department entity)
        {
            var dto = new DepartmentGet
            {
                Id = entity.Id,
                DepartmentName = entity.DepartmentName,
                CreatinDateLog = entity.CreationDateLog,
                UpdateDateLog = entity.UpdateDateLog,
                DeleteDateLog = entity.DeleteDateLog,
                State = entity.State.ToString()
            };

            return dto;
        }
    }
}
