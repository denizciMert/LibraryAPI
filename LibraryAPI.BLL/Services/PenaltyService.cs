using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.PenaltyDTO;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// PenaltyService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to penalty management.
    /// </summary>
    public class PenaltyService : ILibraryServiceManager<PenaltyGet, PenaltyPost, Penalty>
    {
        // Private fields to hold instances of data and mappers.
        private readonly PenaltyData _penaltyData;
        private readonly PenaltyMapper _penaltyMapper;

        /// <summary>
        /// Constructor to initialize the PenaltyService with necessary dependencies.
        /// </summary>
        public PenaltyService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _penaltyData = new PenaltyData(context, userManager);
            _penaltyMapper = new PenaltyMapper();
        }

        /// <summary>
        /// Retrieves all penalties.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<PenaltyGet>>> GetAllAsync()
        {
            try
            {
                var penalties = await _penaltyData.SelectAllFiltered();
                if (penalties.Count == 0)
                {
                    return ServiceResult<IEnumerable<PenaltyGet>>.FailureResult("Ceza verisi bulunmuyor.");
                }
                List<PenaltyGet> penaltyGets = new List<PenaltyGet>();
                foreach (var penalty in penalties)
                {
                    var penaltyGet = _penaltyMapper.MapToDto(penalty);
                    penaltyGets.Add(penaltyGet);
                }
                return ServiceResult<IEnumerable<PenaltyGet>>.SuccessResult(penaltyGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<PenaltyGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all penalties with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Penalty>>> GetAllWithDataAsync()
        {
            try
            {
                var penalties = await _penaltyData.SelectAll();
                if (penalties.Count == 0)
                {
                    return ServiceResult<IEnumerable<Penalty>>.FailureResult("Ceza verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Penalty>>.SuccessResult(penalties);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Penalty>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a penalty by its ID.
        /// </summary>
        public async Task<ServiceResult<PenaltyGet>> GetByIdAsync(int id)
        {
            try
            {
                Penalty? nullPenalty = null;
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == nullPenalty)
                {
                    return ServiceResult<PenaltyGet>.FailureResult("Ceza verisi bulunmuyor.");
                }
                var penaltyGet = _penaltyMapper.MapToDto(penalty);
                return ServiceResult<PenaltyGet>.SuccessResult(penaltyGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a penalty with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Penalty>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Penalty? nullPenalty = null;
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == nullPenalty)
                {
                    return ServiceResult<Penalty>.FailureResult("Ceza verisi bulunmuyor.");
                }
                return ServiceResult<Penalty>.SuccessResult(penalty);
            }
            catch (Exception ex)
            {
                return ServiceResult<Penalty>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new penalty.
        /// </summary>
        public async Task<ServiceResult<PenaltyGet>> AddAsync(PenaltyPost tPost)
        {
            try
            {
                if (await _penaltyData.IsRegistered(tPost))
                {
                    return ServiceResult<PenaltyGet>.FailureResult("Bu ceza zaten eklenmiş.");
                }
                var newPenalty = _penaltyMapper.PostEntity(tPost);
                _penaltyData.AddToContext(newPenalty);
                await _penaltyData.SaveContext();
                await _penaltyData.BanUser(tPost.PenaltiedMemberId);
                var result = await GetByIdAsync(newPenalty.Id);
                return ServiceResult<PenaltyGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing penalty.
        /// </summary>
        public async Task<ServiceResult<PenaltyGet>> UpdateAsync(int id, PenaltyPost tPost)
        {
            try
            {
                if (await _penaltyData.IsRegistered(tPost))
                {
                    return ServiceResult<PenaltyGet>.FailureResult("Bu ceza zaten eklenmiş.");
                }
                Penalty? nullPenalty = null;
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == nullPenalty)
                {
                    return ServiceResult<PenaltyGet>.FailureResult("Ceza verisi bulunmuyor.");
                }
                if (tPost.PenaltiedMemberId != penalty.PenaltiedMembeId)
                {
                    await _penaltyData.UnBanUser(penalty.PenaltiedMembeId);
                    await _penaltyData.BanUser(tPost.PenaltiedMemberId);
                }
                _penaltyMapper.UpdateEntity(penalty, tPost);
                await _penaltyData.SaveContext();
                var updatedPenalty = _penaltyMapper.MapToDto(penalty);
                return ServiceResult<PenaltyGet>.SuccessResult(updatedPenalty);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a penalty by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Penalty? nullPenalty = null;
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == nullPenalty)
                {
                    return ServiceResult<bool>.FailureResult("Ceza verisi bulunmuyor.");
                }
                _penaltyMapper.DeleteEntity(penalty);
                await _penaltyData.SaveContext();
                await _penaltyData.UnBanUser(penalty.PenaltiedMembeId);
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
