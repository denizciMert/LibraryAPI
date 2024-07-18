using System;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
    public class EmployeeMapper
    {
        public Employee MapToEntity(EmployeePost employeePost)
        {
            var employee = new Employee
            {
                ApplicationUser = new ApplicationUser
                {
                    FirstName = employeePost.FirstName,
                    MiddleName = employeePost.MiddleName,
                    LastName = employeePost.LastName,
                    UserName = employeePost.UserName,
                    IdentityNo = employeePost.IdentityNo,
                    DateOfBirth = employeePost.DateOfBirth,
                    Gender = (Gender)employeePost.GenderId,
                    CountryId = employeePost.CountryId,
                    UserRole = (UserRole)employeePost.UserRoleId!,
                    DateOfRegister = DateTime.Now
                },
                Salary = employeePost.Salary,
                TitleId = employeePost.TitleId,
                DepartmentId = employeePost.DepartmentId,
                ShiftId = employeePost.ShiftId
            };
            return employee;
        }

        public EmployeeGet MapToDto(Employee entity)
        {
            var dto = new EmployeeGet
            {
                Id = entity.Id,
                EmployeeName = $"{entity.ApplicationUser?.FirstName} {entity.ApplicationUser?.LastName}",
                IdentityNo = entity.ApplicationUser?.IdentityNo,
                UserName = entity.ApplicationUser?.UserName,
                Email = entity.ApplicationUser?.Email,
                Phone = entity.ApplicationUser?.PhoneNumber,
                DateOfBirth = entity.ApplicationUser?.DateOfBirth,
                Gender = entity.ApplicationUser?.Gender.ToString(),
                Country = entity.ApplicationUser?.Country?.CountryName,
                Title = entity.Title?.TitleName,
                Department = entity.Department?.DepartmentName,
                Salary = entity.Salary,
                Shift = entity.Shift?.ShiftType,
                ImagePath = entity.ApplicationUser?.UserImagePath,
                Addresses = entity.ApplicationUser?.Addresses?.Select(a => a.AddressString + " " + a.District.DistrictName + " " + a.District.City.CityName + " " + a.District.City.Country.CountryName).ToList(),
                Banned = entity.ApplicationUser.Banned,
                UserRole = entity.ApplicationUser?.UserRole.ToString(),
                DateOfRegister = entity.ApplicationUser.DateOfRegister,
                UpdateDateLog = entity.ApplicationUser.UpdateDateLog,
                DeleteDateLog = entity.ApplicationUser.DeleteDateLog,
                State = entity.ApplicationUser.State.ToString()
            };

            return dto;
        }
    }
}

