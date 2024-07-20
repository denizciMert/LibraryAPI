using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.DepartmentDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
	public class DepartmentMapper
	{
        public Department MapToEntity(DepartmentPost dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName
            };

            return department;
        }

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

        public Department UpdateEntity(Department department, DepartmentPost departmentPost)
        {
            department.DepartmentName = department.DepartmentName;
            department.CreationDateLog = department.CreationDateLog;
            department.UpdateDateLog = DateTime.Now;
            department.DeleteDateLog = null;
            department.State = State.Güncellendi;

            return department;
        }

        public Department DeleteEntity(Department department)
        {
            department.DeleteDateLog = DateTime.Now;
            department.State = State.Silindi;

            return department;
        }

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

