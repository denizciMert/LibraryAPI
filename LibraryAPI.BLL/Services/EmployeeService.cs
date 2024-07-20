using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class EmployeeService : ILibraryUserManager
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
/*
private readonly ApplicationDbContext _context;
private readonly EmployeeData _employeeData;
private readonly EmployeeMapper _employeeMapper;

public EmployeeService(ApplicationDbContext context)
{
    _context = context;
    _employeeData = new EmployeeData(_context);
    _employeeMapper = new EmployeeMapper();
}

public async Task<ServiceResult<IEnumerable<EmployeeGet>>> GetAllAsync()
{
    try
    {
        var employees = await _employeeData.SelectAll();
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

public async Task<ServiceResult<EmployeeGet>> GetByIdAsync(int id)
{
    try
    {
        var employee = await _employeeData.SelectForEntity(id);
        if (employee == null)
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

public async Task<ServiceResult<Employee>> GetWithDataByIdAsync(int id)
{
    try
    {
        var employee = await _employeeData.SelectForEntity(id);
        if (employee == null)
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
        var newEmployee = _employeeMapper.PostEntity(tPost);
        _employeeData.AddToContext(newEmployee);
        await _employeeData.SaveContext();
        var result = await GetByIdAsync(newEmployee.Id);
        return ServiceResult<EmployeeGet>.SuccessResult(result.Data);
    }
    catch (Exception ex)
    {
        return ServiceResult<EmployeeGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
    }
}

public async Task<ServiceResult<EmployeeGet>> UpdateAsync(int id, EmployeePost tPost)
{
    try
    {
        var employee = await _employeeData.SelectForEntity(id);
        if (employee == null)
        {
            return ServiceResult<EmployeeGet>.FailureResult("Çalışan verisi bulunmuyor.");
        }
        _employeeMapper.UpdateEntity(employee, tPost);
        await _employeeData.SaveContext();
        var updatedEmployee = _employeeMapper.MapToDto(employee);
        return ServiceResult<EmployeeGet>.SuccessResult(updatedEmployee);
    }
    catch (Exception ex)
    {
        return ServiceResult<EmployeeGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
    }
}

public async Task<ServiceResult<bool>> DeleteAsync(int id)
{
    try
    {
        var employee = await _employeeData.SelectForEntity(id);
        if (employee == null)
        {
            return ServiceResult<bool>.FailureResult("Çalışan verisi bulunmuyor.");
        }
        _employeeData.DeleteFromContext(employee);
        await _employeeData.SaveContext();
        return ServiceResult<bool>.SuccessResult(true);
    }
    catch (Exception ex)
    {
        return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
    }
}
*/