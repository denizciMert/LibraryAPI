using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Mappers
{
    public class EmployeeMapper
    {
        public ApplicationUser PostUser(EmployeePost tPost)
        {
            var user = new ApplicationUser
            {
                FirstName = tPost.FirstName,
                MiddleName = tPost.MiddleName,
                LastName = tPost.LastName,
                UserName = tPost.UserName,
                IdentityNo = tPost.IdentityNo,
                DateOfBirth = tPost.DateOfBirth,
                Gender = (Gender)tPost.GenderId,
                CountryId = tPost.CountryId,
                UserRole = (UserRole)tPost.UserRoleId!,
                DateOfRegister = DateTime.Now,
                Email = tPost.Email,
                PhoneNumber = tPost.Phone,
                Banned = false,
                UserImagePath = tPost.UserImagePath,
                UpdateDateLog = null,
                DeleteDateLog = null,
                State = State.Eklendi
            };

            return user;
        }

        public Employee PostEmployee(ApplicationUser user, EmployeePost employeePost)
        {
            var emlpoyee = new Employee
            {
                Id = user.Id,
                Salary = employeePost.Salary,
                TitleId = employeePost.TitleId,
                DepartmentId = employeePost.DepartmentId,
                ShiftId = employeePost.ShiftId
            };

            return emlpoyee;
        }

        public ApplicationUser UpdateUser(ApplicationUser user, EmployeePost tPost)
        {
            user.FirstName = tPost.FirstName;
            user.MiddleName = tPost.MiddleName;
            user.LastName = tPost.LastName;
            user.UserName = tPost.UserName;
            user.IdentityNo = tPost.IdentityNo;
            user.DateOfBirth = tPost.DateOfBirth;
            user.Gender = (Gender)tPost.GenderId;
            user.CountryId = tPost.CountryId;
            user.UserRole = (UserRole)tPost.UserRoleId!;
            user.DateOfRegister = user.DateOfRegister;
            user.Email = tPost.Email;
            user.PhoneNumber = tPost.Phone;
            user.Banned = false;
            user.UserImagePath = tPost.UserImagePath;
            user.UpdateDateLog = DateTime.Now;
            user.DeleteDateLog = null;
            user.State = State.Güncellendi;

            return user;
        }

        public Employee UpdateEmployee(Employee employee, EmployeePost employeePost)
        {
            employee.Salary = employeePost.Salary;
            employee.TitleId = employeePost.TitleId;
            employee.DepartmentId = employeePost.DepartmentId;
            employee.ShiftId = employeePost.ShiftId;

            return employee;
        }

        public Employee DeleteEntity(Employee employee)
        {
            employee.ApplicationUser.DeleteDateLog = DateTime.Now;
            employee.ApplicationUser.State = State.Silindi;

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
                Addresses = entity.ApplicationUser?.Addresses?.Select(a => a.AddressString + " - " + a.District.DistrictName + " - " + a.District.City.CityName + " - " + a.District.City.Country.CountryName).ToList(),
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

