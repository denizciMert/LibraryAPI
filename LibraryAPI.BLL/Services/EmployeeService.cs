using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.BLL.Services
{
    public class EmployeeService : ILibraryUserManager<EmployeeGet,EmployeePost,Employee>
    {
        private readonly EmployeeData _employeeData;
        private readonly EmployeeMapper _employeeMapper;

        public EmployeeService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _employeeData = new EmployeeData(context, userManager);
            _employeeMapper = new EmployeeMapper();
        }

        public async Task<ServiceResult<IEnumerable<EmployeeGet>>> GetAllAsync()
        {
            try
            {
                var employees = await _employeeData.SelectAllFiltered();
                if (employees == null || employees.Count == 0)
                {
                    return ServiceResult<IEnumerable<EmployeeGet>>.FailureResult("Çalışan verisi bulunmuyor.");
                }
                List<EmployeeGet> employeeGets = new List<EmployeeGet>();
                foreach (var employee in employees)
                {
                    var employeeGet = _employeeMapper.MapToDto(employee);
                    employeeGets.Add(employeeGet);
                }
                return ServiceResult<IEnumerable<EmployeeGet>>.SuccessResult(employeeGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<EmployeeGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Employee>>> GetAllWithDataAsync()
        {
            try
            {
                var employees = await _employeeData.SelectAll();
                if (employees == null || employees.Count == 0)
                {
                    return ServiceResult<IEnumerable<Employee>>.FailureResult("Çalışan verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Employee>>.SuccessResult(employees);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Employee>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<EmployeeGet>> GetByIdAsync(string id)
        {
            try
            {
                var employee = await _employeeData.SelectForUser(id);
                if (employee.ApplicationUser == null)
                {
                    return ServiceResult<EmployeeGet>.FailureResult("Çalışan verisi bulunmuyor.");
                }
                var employeeGet = _employeeMapper.MapToDto(employee);
                return ServiceResult<EmployeeGet>.SuccessResult(employeeGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<EmployeeGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Employee>> GetWithDataByIdAsync(string id)
        {
            try
            {
                var employee = await _employeeData.SelectForUser(id);
                if (employee.ApplicationUser == null)
                {
                    return ServiceResult<Employee>.FailureResult("Çalışan verisi bulunmuyor.");
                }
                return ServiceResult<Employee>.SuccessResult(employee);
            }
            catch (Exception ex)
            {
                return ServiceResult<Employee>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<EmployeeGet>> AddAsync(EmployeePost tPost)
        {
            try
            {
                if (await _employeeData.IsRegistered(tPost))
                {
                    return ServiceResult<EmployeeGet>.FailureResult("Bu çalışan zaten eklenmiş.");
                }

                var newUser = _employeeMapper.PostUser(tPost);
                await _employeeData.SaveUser(newUser, tPost.Password);
                var newEmployee = _employeeMapper.PostEmployee(newUser, tPost);
                _employeeData.AddToContext(newEmployee);
                await _employeeData.SaveContext();
                await _employeeData.AddRoleToUser(newEmployee.ApplicationUser, ((UserRole)tPost.UserRoleId).ToString());
                var result = await GetByIdAsync(newEmployee.Id);
                return ServiceResult<EmployeeGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<EmployeeGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<EmployeeGet>> UpdateAsync(string id, EmployeePost tPost)
        {
            try
            {
                var employee = await _employeeData.SelectForUser(id);
                if (employee.ApplicationUser == null)
                {
                    return ServiceResult<EmployeeGet>.FailureResult("Çalışan verisi bulunmuyor.");
                }
                _employeeMapper.UpdateUser(employee.ApplicationUser, tPost);
                await _employeeData.UpdateUser(employee.ApplicationUser);
                _employeeMapper.UpdateEmployee(employee, tPost);
                await _employeeData.SaveContext();
                var result = _employeeMapper.MapToDto(employee);
                return ServiceResult<EmployeeGet>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<EmployeeGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(string id)
        {
            try
            {
                var employee = await _employeeData.SelectForUser(id);
                if (employee.ApplicationUser == null)
                {
                    return ServiceResult<bool>.FailureResult("Çalışan verisi bulunmuyor.");
                }

                _employeeMapper.DeleteEntity(employee);
                await _employeeData.DeleteUser(employee.ApplicationUser);
                await _employeeData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
