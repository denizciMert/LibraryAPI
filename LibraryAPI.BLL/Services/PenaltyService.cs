using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.PenaltyDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class PenaltyService : ILibraryServiceManager<PenaltyGet,PenaltyPost,Penalty>
    {
        private readonly PenaltyData _penaltyData;
        private readonly PenaltyMapper _penaltyMapper;

        public PenaltyService(ApplicationDbContext context)
        {
            _penaltyData = new PenaltyData(context);
            _penaltyMapper = new PenaltyMapper();
        }

        public async Task<ServiceResult<IEnumerable<PenaltyGet>>> GetAllAsync()
        {
            try
            {
                var penalties = await _penaltyData.SelectAllFiltered();
                if (penalties == null || penalties.Count == 0)
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
                return ServiceResult<IEnumerable<PenaltyGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Penalty>>> GetAllWithDataAsync()
        {
            try
            {
                var penalties = await _penaltyData.SelectAll();
                if (penalties == null || penalties.Count == 0)
                {
                    return ServiceResult<IEnumerable<Penalty>>.FailureResult("Ceza verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Penalty>>.SuccessResult(penalties);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Penalty>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<PenaltyGet>> GetByIdAsync(int id)
        {
            try
            {
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == null)
                {
                    return ServiceResult<PenaltyGet>.FailureResult("Ceza verisi bulunmuyor.");
                }
                var penaltyGet = _penaltyMapper.MapToDto(penalty);
                return ServiceResult<PenaltyGet>.SuccessResult(penaltyGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Penalty>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == null)
                {
                    return ServiceResult<Penalty>.FailureResult("Ceza verisi bulunmuyor.");
                }
                return ServiceResult<Penalty>.SuccessResult(penalty);
            }
            catch (Exception ex)
            {
                return ServiceResult<Penalty>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

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
                var result = await GetByIdAsync(newPenalty.Id);
                return ServiceResult<PenaltyGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        public async Task<ServiceResult<PenaltyGet>> UpdateAsync(int id, PenaltyPost tPost)
        {
            try
            {
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == null)
                {
                    return ServiceResult<PenaltyGet>.FailureResult("Ceza verisi bulunmuyor.");
                }
                _penaltyMapper.UpdateEntity(penalty, tPost);
                await _penaltyData.SaveContext();
                var updatedPenalty = _penaltyMapper.MapToDto(penalty);
                return ServiceResult<PenaltyGet>.SuccessResult(updatedPenalty);
            }
            catch (Exception ex)
            {
                return ServiceResult<PenaltyGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var penalty = await _penaltyData.SelectForEntity(id);
                if (penalty == null)
                {
                    return ServiceResult<bool>.FailureResult("Ceza verisi bulunmuyor.");
                }
                _penaltyMapper.DeleteEntity(penalty);
                await _penaltyData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
