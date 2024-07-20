using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.DepartmentDTO;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Core;
using LibraryAPI.DAL;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL.Data;

namespace LibraryAPI.BLL.Services
{
    public class DepartmentService : ILibraryServiceManager<DepartmentGet, DepartmentPost, Department>
    {
        private readonly DepartmentData _departmentData;
        private readonly DepartmentMapper _departmentMapper;

        public DepartmentService(ApplicationDbContext context)
        {
            _departmentData = new DepartmentData(context);
            _departmentMapper = new DepartmentMapper();
        }

        public async Task<ServiceResult<IEnumerable<DepartmentGet>>> GetAllAsync()
        {
            try
            {
                var departments = await _departmentData.SelectAll();
                if (departments == null || departments.Count == 0)
                {
                    return ServiceResult<IEnumerable<DepartmentGet>>.FailureResult("Bölüm verisi bulunmuyor.");
                }
                List<DepartmentGet> departmentGets = new List<DepartmentGet>();
                foreach (var department in departments)
                {
                    var departmentGet = _departmentMapper.MapToDto(department);
                    departmentGets.Add(departmentGet);
                }
                return ServiceResult<IEnumerable<DepartmentGet>>.SuccessResult(departmentGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<DepartmentGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Department>>> GetAllWithDataAsync()
        {
            try
            {
                var departments = await _departmentData.SelectAll();
                if (departments == null || departments.Count == 0)
                {
                    return ServiceResult<IEnumerable<Department>>.FailureResult("Bölüm verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Department>>.SuccessResult(departments);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Department>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<DepartmentGet>> GetByIdAsync(int id)
        {
            try
            {
                var department = await _departmentData.SelectForEntity(id);
                if (department == null)
                {
                    return ServiceResult<DepartmentGet>.FailureResult("Bölüm verisi bulunmuyor.");
                }
                var departmentGet = _departmentMapper.MapToDto(department);
                return ServiceResult<DepartmentGet>.SuccessResult(departmentGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<DepartmentGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Department>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var department = await _departmentData.SelectForEntity(id);
                if (department == null)
                {
                    return ServiceResult<Department>.FailureResult("Bölüm verisi bulunmuyor.");
                }
                return ServiceResult<Department>.SuccessResult(department);
            }
            catch (Exception ex)
            {
                return ServiceResult<Department>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<DepartmentGet>> AddAsync(DepartmentPost tPost)
        {
            try
            {
                if (await _departmentData.IsRegistered(tPost))
                {
                    return ServiceResult<DepartmentGet>.FailureResult("Bu bölüm zaten eklenmiş.");
                }
                var newDepartment = _departmentMapper.PostEntity(tPost);
                _departmentData.AddToContext(newDepartment);
                await _departmentData.SaveContext();
                var result = await GetByIdAsync(newDepartment.Id);
                return ServiceResult<DepartmentGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<DepartmentGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<DepartmentGet>> UpdateAsync(int id, DepartmentPost tPost)
        {
            try
            {
                var department = await _departmentData.SelectForEntity(id);
                if (department == null)
                {
                    return ServiceResult<DepartmentGet>.FailureResult("Bölüm verisi bulunmuyor.");
                }
                _departmentMapper.UpdateEntity(department, tPost);
                await _departmentData.SaveContext();
                var newDepartment = _departmentMapper.MapToDto(department);
                return ServiceResult<DepartmentGet>.SuccessResult(newDepartment);
            }
            catch (Exception ex)
            {
                return ServiceResult<DepartmentGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var department = await _departmentData.SelectForEntity(id);
                if (department == null)
                {
                    return ServiceResult<bool>.FailureResult("Bölüm verisi bulunmuyor.");
                }
                _departmentMapper.DeleteEntity(department);
                await _departmentData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
