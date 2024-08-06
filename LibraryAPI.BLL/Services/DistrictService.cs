using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.DistrictDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// DistrictService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to district management.
    /// </summary>
    public class DistrictService : ILibraryServiceManager<DistrictGet, DistrictPost, District>
    {
        // Private fields to hold instances of data and mappers.
        private readonly DistrictData _districtData;
        private readonly DistrictMapper _districtMapper;

        /// <summary>
        /// Constructor to initialize the DistrictService with necessary dependencies.
        /// </summary>
        public DistrictService(ApplicationDbContext context)
        {
            _districtData = new DistrictData(context);
            _districtMapper = new DistrictMapper();
        }

        /// <summary>
        /// Retrieves all districts.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<DistrictGet>>> GetAllAsync()
        {
            try
            {
                var districts = await _districtData.SelectAllFiltered();
                if (districts.Count == 0)
                {
                    return ServiceResult<IEnumerable<DistrictGet>>.FailureResult("Bölge verisi bulunmuyor.");
                }
                List<DistrictGet> districtGets = new List<DistrictGet>();
                foreach (var district in districts)
                {
                    var districtGet = _districtMapper.MapToDto(district);
                    districtGets.Add(districtGet);
                }
                return ServiceResult<IEnumerable<DistrictGet>>.SuccessResult(districtGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<DistrictGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all districts with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<District>>> GetAllWithDataAsync()
        {
            try
            {
                var districts = await _districtData.SelectAll();
                if (districts.Count == 0)
                {
                    return ServiceResult<IEnumerable<District>>.FailureResult("Bölge verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<District>>.SuccessResult(districts);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<District>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a district by its ID.
        /// </summary>
        public async Task<ServiceResult<DistrictGet>> GetByIdAsync(int id)
        {
            try
            {
                District? nullDistrict = null;
                var district = await _districtData.SelectForEntity(id);
                if (district == nullDistrict)
                {
                    return ServiceResult<DistrictGet>.FailureResult("Bölge verisi bulunmuyor.");
                }
                var districtGet = _districtMapper.MapToDto(district);
                return ServiceResult<DistrictGet>.SuccessResult(districtGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<DistrictGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a district with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<District>> GetWithDataByIdAsync(int id)
        {
            try
            {
                District? nullDistrict = null;
                var district = await _districtData.SelectForEntity(id);
                if (district == nullDistrict)
                {
                    return ServiceResult<District>.FailureResult("Bölge verisi bulunmuyor.");
                }
                return ServiceResult<District>.SuccessResult(district);
            }
            catch (Exception ex)
            {
                return ServiceResult<District>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new district.
        /// </summary>
        public async Task<ServiceResult<DistrictGet>> AddAsync(DistrictPost tPost)
        {
            try
            {
                if (await _districtData.IsRegistered(tPost))
                {
                    return ServiceResult<DistrictGet>.FailureResult("Bu bölge zaten eklenmiş.");
                }
                var newDistrict = _districtMapper.PostEntity(tPost);
                _districtData.AddToContext(newDistrict);
                await _districtData.SaveContext();
                var result = await GetByIdAsync(newDistrict.Id);
                return ServiceResult<DistrictGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<DistrictGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing district.
        /// </summary>
        public async Task<ServiceResult<DistrictGet>> UpdateAsync(int id, DistrictPost tPost)
        {
            try
            {
                District? nullDistrict = null;
                var district = await _districtData.SelectForEntity(id);
                if (district == nullDistrict)
                {
                    return ServiceResult<DistrictGet>.FailureResult("Bölge verisi bulunmuyor.");
                }
                if (await _districtData.IsRegistered(tPost))
                {
                    return ServiceResult<DistrictGet>.FailureResult("Bu bölge zaten eklenmiş.");
                }
                _districtMapper.UpdateEntity(district, tPost);
                await _districtData.SaveContext();
                var updatedDistrict = _districtMapper.MapToDto(district);
                return ServiceResult<DistrictGet>.SuccessResult(updatedDistrict);
            }
            catch (Exception ex)
            {
                return ServiceResult<DistrictGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a district by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                District? nullDistrict = null;
                var district = await _districtData.SelectForEntity(id);
                if (district == nullDistrict)
                {
                    return ServiceResult<bool>.FailureResult("Bölge verisi bulunmuyor.");
                }

                _districtMapper.DeleteEntity(district);
                await _districtData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
