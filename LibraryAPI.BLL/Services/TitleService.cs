using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class TitleService : ILibraryServiceManager<TitleGet,TitlePost,Title>
    {
        private readonly TitleData _titleData;
        private readonly TitleMapper _titleMapper;

        public TitleService(ApplicationDbContext context)
        {
            _titleData = new TitleData(context);
            _titleMapper = new TitleMapper();
        }

        public async Task<ServiceResult<IEnumerable<TitleGet>>> GetAllAsync()
        {
            try
            {
                var titles = await _titleData.SelectAll();
                if (titles == null || titles.Count == 0)
                {
                    return ServiceResult<IEnumerable<TitleGet>>.FailureResult("Başlık verisi bulunmuyor.");
                }
                List<TitleGet> titleGets = new List<TitleGet>();
                foreach (var title in titles)
                {
                    var titleGet = _titleMapper.MapToDto(title);
                    titleGets.Add(titleGet);
                }
                return ServiceResult<IEnumerable<TitleGet>>.SuccessResult(titleGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<TitleGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Title>>> GetAllWithDataAsync()
        {
            try
            {
                var titles = await _titleData.SelectAll();
                if (titles == null || titles.Count == 0)
                {
                    return ServiceResult<IEnumerable<Title>>.FailureResult("Başlık verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Title>>.SuccessResult(titles);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Title>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<TitleGet>> GetByIdAsync(int id)
        {
            try
            {
                var title = await _titleData.SelectForEntity(id);
                if (title == null)
                {
                    return ServiceResult<TitleGet>.FailureResult("Başlık verisi bulunmuyor.");
                }
                var titleGet = _titleMapper.MapToDto(title);
                return ServiceResult<TitleGet>.SuccessResult(titleGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<TitleGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Title>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var title = await _titleData.SelectForEntity(id);
                if (title == null)
                {
                    return ServiceResult<Title>.FailureResult("Başlık verisi bulunmuyor.");
                }
                return ServiceResult<Title>.SuccessResult(title);
            }
            catch (Exception ex)
            {
                return ServiceResult<Title>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<TitleGet>> AddAsync(TitlePost tPost)
        {
            try
            {
                if (await _titleData.IsRegistered(tPost))
                {
                    return ServiceResult<TitleGet>.FailureResult("Bu başlık zaten eklenmiş.");
                }
                var newTitle = _titleMapper.PostEntity(tPost);
                _titleData.AddToContext(newTitle);
                await _titleData.SaveContext();
                var result = await GetByIdAsync(newTitle.Id);
                return ServiceResult<TitleGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<TitleGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<TitleGet>> UpdateAsync(int id, TitlePost tPost)
        {
            try
            {
                var title = await _titleData.SelectForEntity(id);
                if (title == null)
                {
                    return ServiceResult<TitleGet>.FailureResult("Başlık verisi bulunmuyor.");
                }
                _titleMapper.UpdateEntity(title, tPost);
                await _titleData.SaveContext();
                var updatedTitle = _titleMapper.MapToDto(title);
                return ServiceResult<TitleGet>.SuccessResult(updatedTitle);
            }
            catch (Exception ex)
            {
                return ServiceResult<TitleGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var title = await _titleData.SelectForEntity(id);
                if (title == null)
                {
                    return ServiceResult<bool>.FailureResult("Başlık verisi bulunmuyor.");
                }
                _titleMapper.DeleteEntity(title);
                await _titleData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
