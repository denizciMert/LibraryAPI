using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.ShiftDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// ShiftService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to shift management.
    /// </summary>
    public class ShiftService : ILibraryServiceManager<ShiftGet,ShiftPost,Shift>
    {
        // Private fields to hold instances of data and mappers.
        private readonly ShiftData _shiftData;
        private readonly ShiftMapper _shiftMapper;

        /// <summary>
        /// Constructor to initialize the ShiftService with necessary dependencies.
        /// </summary>
        public ShiftService(ApplicationDbContext context)
        {
            _shiftData = new ShiftData(context);
            _shiftMapper = new ShiftMapper();
        }

        /// <summary>
        /// Retrieves all shifts.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<ShiftGet>>> GetAllAsync()
        {
            try
            {
                var shifts = await _shiftData.SelectAllFiltered();
                if (shifts.Count == 0)
                {
                    return ServiceResult<IEnumerable<ShiftGet>>.FailureResult("Vardiya verisi bulunmuyor.");
                }
                List<ShiftGet> shiftGets = new List<ShiftGet>();
                foreach (var shift in shifts)
                {
                    var shiftGet = _shiftMapper.MapToDto(shift);
                    shiftGets.Add(shiftGet);
                }
                return ServiceResult<IEnumerable<ShiftGet>>.SuccessResult(shiftGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ShiftGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all shifts with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Shift>>> GetAllWithDataAsync()
        {
            try
            {
                var shifts = await _shiftData.SelectAll();
                if (shifts.Count == 0)
                {
                    return ServiceResult<IEnumerable<Shift>>.FailureResult("Vardiya verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Shift>>.SuccessResult(shifts);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Shift>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a shift by its ID.
        /// </summary>
        public async Task<ServiceResult<ShiftGet>> GetByIdAsync(int id)
        {
            try
            {
                Shift? nullShift = null;
                var shift = await _shiftData.SelectForEntity(id);
                if (shift == nullShift)
                {
                    return ServiceResult<ShiftGet>.FailureResult("Vardiya verisi bulunmuyor.");
                }
                var shiftGet = _shiftMapper.MapToDto(shift);
                return ServiceResult<ShiftGet>.SuccessResult(shiftGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<ShiftGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a shift with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Shift>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Shift? nullShift = null;
                var shift = await _shiftData.SelectForEntity(id);
                if (shift == nullShift)
                {
                    return ServiceResult<Shift>.FailureResult("Vardiya verisi bulunmuyor.");
                }
                return ServiceResult<Shift>.SuccessResult(shift);
            }
            catch (Exception ex)
            {
                return ServiceResult<Shift>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new shift.
        /// </summary>
        public async Task<ServiceResult<ShiftGet>> AddAsync(ShiftPost tPost)
        {
            try
            {
                if (await _shiftData.IsRegistered(tPost))
                {
                    return ServiceResult<ShiftGet>.FailureResult("Bu vardiya zaten eklenmiş.");
                }
                var newShift = _shiftMapper.PostEntity(tPost);
                _shiftData.AddToContext(newShift);
                await _shiftData.SaveContext();
                var result = await GetByIdAsync(newShift.Id);
                return ServiceResult<ShiftGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<ShiftGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing shift.
        /// </summary>
        public async Task<ServiceResult<ShiftGet>> UpdateAsync(int id, ShiftPost tPost)
        {
            try
            {
                if (await _shiftData.IsRegistered(tPost))
                {
                    return ServiceResult<ShiftGet>.FailureResult("Bu vardiya zaten eklenmiş.");
                }
                Shift? nullShift = null;
                var shift = await _shiftData.SelectForEntity(id);
                if (shift == nullShift)
                {
                    return ServiceResult<ShiftGet>.FailureResult("Vardiya verisi bulunmuyor.");
                }
                _shiftMapper.UpdateEntity(shift, tPost);
                await _shiftData.SaveContext();
                var updatedShift = _shiftMapper.MapToDto(shift);
                return ServiceResult<ShiftGet>.SuccessResult(updatedShift);
            }
            catch (Exception ex)
            {
                return ServiceResult<ShiftGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a shift by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Shift? nullShift = null;
                var shift = await _shiftData.SelectForEntity(id);
                if (shift == nullShift)
                {
                    return ServiceResult<bool>.FailureResult("Vardiya verisi bulunmuyor.");
                }
                _shiftMapper.DeleteEntity(shift);
                await _shiftData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

    }
}
