using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// Service class for managing Title operations
    /// </summary>
    public class TitleService : ILibraryServiceManager<TitleGet, TitlePost, Title>
    {
        // Private fields for data access and mapping
        private readonly TitleData _titleData;
        private readonly TitleMapper _titleMapper;

        /// <summary>
        /// Constructor to initialize the data access and mapper
        /// </summary>
        public TitleService(ApplicationDbContext context)
        {
            _titleData = new TitleData(context);
            _titleMapper = new TitleMapper();
        }

        /// <summary>
        /// Method to get all titles
        /// </summary>
        public async Task<ServiceResult<IEnumerable<TitleGet>>> GetAllAsync()
        {
            try
            {
                var titles = await _titleData.SelectAllFiltered();
                if (titles.Count == 0)
                {
                    return ServiceResult<IEnumerable<TitleGet>>.FailureResult("Ünvan verisi bulunmuyor.");
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
                return ServiceResult<IEnumerable<TitleGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to get all titles with data
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Title>>> GetAllWithDataAsync()
        {
            try
            {
                var titles = await _titleData.SelectAll();
                if (titles.Count == 0)
                {
                    return ServiceResult<IEnumerable<Title>>.FailureResult("Ünvan verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Title>>.SuccessResult(titles);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Title>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to get a title by ID
        /// </summary>
        public async Task<ServiceResult<TitleGet>> GetByIdAsync(int id)
        {
            try
            {
                Title? nullTitle = null;
                var title = await _titleData.SelectForEntity(id);
                if (title == nullTitle)
                {
                    return ServiceResult<TitleGet>.FailureResult("Ünvan verisi bulunmuyor.");
                }
                var titleGet = _titleMapper.MapToDto(title);
                return ServiceResult<TitleGet>.SuccessResult(titleGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<TitleGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to get a title with data by ID
        /// </summary>
        public async Task<ServiceResult<Title>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Title? nullTitle = null;
                var title = await _titleData.SelectForEntity(id);
                if (title == nullTitle)
                {
                    return ServiceResult<Title>.FailureResult("Ünvan verisi bulunmuyor.");
                }
                return ServiceResult<Title>.SuccessResult(title);
            }
            catch (Exception ex)
            {
                return ServiceResult<Title>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to add a new title
        /// </summary>
        public async Task<ServiceResult<TitleGet>> AddAsync(TitlePost tPost)
        {
            try
            {
                if (await _titleData.IsRegistered(tPost))
                {
                    return ServiceResult<TitleGet>.FailureResult("Bu ünvan zaten eklenmiş.");
                }
                var newTitle = _titleMapper.PostEntity(tPost);
                _titleData.AddToContext(newTitle);
                await _titleData.SaveContext();
                var result = await GetByIdAsync(newTitle.Id);
                return ServiceResult<TitleGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<TitleGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to update an existing title
        /// </summary>
        public async Task<ServiceResult<TitleGet>> UpdateAsync(int id, TitlePost tPost)
        {
            try
            {
                if (await _titleData.IsRegistered(tPost))
                {
                    return ServiceResult<TitleGet>.FailureResult("Bu ünvan zaten eklenmiş.");
                }
                Title? nullTitle = null;
                var title = await _titleData.SelectForEntity(id);
                if (title == nullTitle)
                {
                    return ServiceResult<TitleGet>.FailureResult("Ünvan verisi bulunmuyor.");
                }
                _titleMapper.UpdateEntity(title, tPost);
                await _titleData.SaveContext();
                var updatedTitle = _titleMapper.MapToDto(title);
                return ServiceResult<TitleGet>.SuccessResult(updatedTitle);
            }
            catch (Exception ex)
            {
                return ServiceResult<TitleGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to delete a title by ID
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Title? nullTitle = null;
                var title = await _titleData.SelectForEntity(id);
                if (title == nullTitle)
                {
                    return ServiceResult<bool>.FailureResult("Ünvan verisi bulunmuyor.");
                }
                _titleMapper.DeleteEntity(title);
                await _titleData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}