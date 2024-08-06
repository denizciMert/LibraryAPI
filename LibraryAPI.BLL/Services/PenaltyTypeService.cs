using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// PenaltyTypeService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to penalty type management.
    /// </summary>
    public class PenaltyTypeService : ILibraryServiceManager<PenaltyTypeGet, PenaltyTypePost, PenaltyType>
    {
        // Private fields to hold instances of data and mappers.
        private readonly PenaltyTypeData _penaltyTypeData;
        private readonly PenaltyTypeMapper _penaltyTypeMapper;

        /// <summary>
        /// Constructor to initialize the PenaltyTypeService with necessary dependencies.
        /// </summary>
        public PenaltyTypeService(ApplicationDbContext context)
        {
            _penaltyTypeData = new PenaltyTypeData(context);
            _penaltyTypeMapper = new PenaltyTypeMapper();
        }

        /// <summary>
        /// Retrieves all penalty types.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<PenaltyTypeGet>>> GetAllAsync()
        {
            try
            {
                var penaltyTypes = await _penaltyTypeData.SelectAllFiltered();
                if (penaltyTypes.Count == 0)
                {
                    return ServiceResult<IEnumerable<PenaltyTypeGet>>.FailureResult("Ceza türü verisi bulunmuyor.");
                }
                List<PenaltyTypeGet> penaltyTypeGets = new List<PenaltyTypeGet>();
                foreach (var penaltyType in penaltyTypes)
                {
                    var penaltyTypeGet = _penaltyTypeMapper.MapToDto(penaltyType);
                    penaltyTypeGets.Add(penaltyTypeGet);
                }
                return ServiceResult<IEnumerable<PenaltyTypeGet>>.SuccessResult(penaltyTypeGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<PenaltyTypeGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all penalty types with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<PenaltyType>>> GetAllWithDataAsync()
        {
            try
            {
                var penaltyTypes = await _penaltyTypeData.SelectAll();
                if (penaltyTypes.Count == 0)
                {
                    return ServiceResult<IEnumerable<PenaltyType>>.FailureResult("Ceza türü verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<PenaltyType>>.SuccessResult(penaltyTypes);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<PenaltyType>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a penalty type by its ID.
        /// </summary>
        public async Task<ServiceResult<PenaltyTypeGet>> GetByIdAsync(int id)
        {
            try
            {
                PenaltyType? nullPenaltyType = null;
                var penaltyType = await _penaltyTypeData.SelectForEntity(id);
                if (penaltyType == nullPenaltyType)
                {
                    return ServiceResult<PenaltyTypeGet>.FailureResult("Ceza türü verisi bulunmuyor.");
                }
                var penaltyTypeGet = _penaltyTypeMapper.MapToDto(penaltyType);
                return ServiceResult<PenaltyTypeGet>.SuccessResult(penaltyTypeGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyTypeGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a penalty type with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<PenaltyType>> GetWithDataByIdAsync(int id)
        {
            try
            {
                PenaltyType? nullPenaltyType = null;
                var penaltyType = await _penaltyTypeData.SelectForEntity(id);
                if (penaltyType == nullPenaltyType)
                {
                    return ServiceResult<PenaltyType>.FailureResult("Ceza türü verisi bulunmuyor.");
                }
                return ServiceResult<PenaltyType>.SuccessResult(penaltyType);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyType>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new penalty type.
        /// </summary>
        public async Task<ServiceResult<PenaltyTypeGet>> AddAsync(PenaltyTypePost tPost)
        {
            try
            {
                if (await _penaltyTypeData.IsRegistered(tPost))
                {
                    return ServiceResult<PenaltyTypeGet>.FailureResult("Bu ceza türü zaten eklenmiş.");
                }
                var newPenaltyType = _penaltyTypeMapper.PostEntity(tPost);
                _penaltyTypeData.AddToContext(newPenaltyType);
                await _penaltyTypeData.SaveContext();
                var result = await GetByIdAsync(newPenaltyType.Id);
                return ServiceResult<PenaltyTypeGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyTypeGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing penalty type.
        /// </summary>
        public async Task<ServiceResult<PenaltyTypeGet>> UpdateAsync(int id, PenaltyTypePost tPost)
        {
            try
            {
                if (await _penaltyTypeData.IsRegistered(tPost))
                {
                    return ServiceResult<PenaltyTypeGet>.FailureResult("Bu ceza türü zaten eklenmiş.");
                }
                PenaltyType? nullPenaltyType = null;
                var penaltyType = await _penaltyTypeData.SelectForEntity(id);
                if (penaltyType == nullPenaltyType)
                {
                    return ServiceResult<PenaltyTypeGet>.FailureResult("Ceza türü verisi bulunmuyor.");
                }
                _penaltyTypeMapper.UpdateEntity(penaltyType, tPost);
                await _penaltyTypeData.SaveContext();
                var updatedPenaltyType = _penaltyTypeMapper.MapToDto(penaltyType);
                return ServiceResult<PenaltyTypeGet>.SuccessResult(updatedPenaltyType);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyTypeGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a penalty type by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                PenaltyType? nullPenaltyType = null;
                var penaltyType = await _penaltyTypeData.SelectForEntity(id);
                if (penaltyType == nullPenaltyType)
                {
                    return ServiceResult<bool>.FailureResult("Ceza türü verisi bulunmuyor.");
                }
                _penaltyTypeMapper.DeleteEntity(penaltyType);
                await _penaltyTypeData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
