using LibraryAPI.Entities.DTOs.DepartmentDTO;
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

