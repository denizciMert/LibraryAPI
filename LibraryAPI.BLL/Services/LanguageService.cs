using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.LanguageDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// LanguageService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to language management.
    /// </summary>
    public class LanguageService : ILibraryServiceManager<LanguageGet, LanguagePost, Language>
    {
        // Private fields to hold instances of data and mappers.
        private readonly LanguageData _languageData;
        private readonly LanguageMapper _languageMapper;

        /// <summary>
        /// Constructor to initialize the LanguageService with necessary dependencies.
        /// </summary>
        public LanguageService(ApplicationDbContext context)
        {
            _languageData = new LanguageData(context);
            _languageMapper = new LanguageMapper();
        }

        /// <summary>
        /// Retrieves all languages.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<LanguageGet>>> GetAllAsync()
        {
            try
            {
                var languages = await _languageData.SelectAllFiltered();
                if (languages.Count == 0)
                {
                    return ServiceResult<IEnumerable<LanguageGet>>.FailureResult("Dil verisi bulunmuyor.");
                }
                List<LanguageGet> languageGets = new List<LanguageGet>();
                foreach (var language in languages)
                {
                    var languageGet = _languageMapper.MapToDto(language);
                    languageGets.Add(languageGet);
                }
                return ServiceResult<IEnumerable<LanguageGet>>.SuccessResult(languageGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<LanguageGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all languages with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Language>>> GetAllWithDataAsync()
        {
            try
            {
                var languages = await _languageData.SelectAll();
                if (languages.Count == 0)
                {
                    return ServiceResult<IEnumerable<Language>>.FailureResult("Dil verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Language>>.SuccessResult(languages);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Language>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a language by its ID.
        /// </summary>
        public async Task<ServiceResult<LanguageGet>> GetByIdAsync(int id)
        {
            try
            {
                Language? nulLanguage = null;
                var language = await _languageData.SelectForEntity(id);
                if (language == nulLanguage)
                {
                    return ServiceResult<LanguageGet>.FailureResult("Dil verisi bulunmuyor.");
                }
                var languageGet = _languageMapper.MapToDto(language);
                return ServiceResult<LanguageGet>.SuccessResult(languageGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<LanguageGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a language with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Language>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Language? nulLanguage = null;
                var language = await _languageData.SelectForEntity(id);
                if (language == nulLanguage)
                {
                    return ServiceResult<Language>.FailureResult("Dil verisi bulunmuyor.");
                }
                return ServiceResult<Language>.SuccessResult(language);
            }
            catch (Exception ex)
            {
                return ServiceResult<Language>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new language.
        /// </summary>
        public async Task<ServiceResult<LanguageGet>> AddAsync(LanguagePost tPost)
        {
            try
            {
                if (await _languageData.IsRegistered(tPost))
                {
                    return ServiceResult<LanguageGet>.FailureResult("Bu dil zaten eklenmiş.");
                }
                var newLanguage = _languageMapper.PostEntity(tPost);
                _languageData.AddToContext(newLanguage);
                await _languageData.SaveContext();
                var result = await GetByIdAsync(newLanguage.Id);
                return ServiceResult<LanguageGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<LanguageGet>.FailureResult($"Bir hata ADD oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing language.
        /// </summary>
        public async Task<ServiceResult<LanguageGet>> UpdateAsync(int id, LanguagePost tPost)
        {
            try
            {
                if (await _languageData.IsRegistered(tPost))
                {
                    return ServiceResult<LanguageGet>.FailureResult("Bu dil zaten eklenmiş.");
                }
                Language? nulLanguage = null;
                var language = await _languageData.SelectForEntity(id);
                if (language == nulLanguage)
                {
                    return ServiceResult<LanguageGet>.FailureResult("Dil verisi bulunmuyor.");
                }
                _languageMapper.UpdateEntity(language, tPost);
                await _languageData.SaveContext();
                var updatedLanguage = _languageMapper.MapToDto(language);
                return ServiceResult<LanguageGet>.SuccessResult(updatedLanguage);
            }
            catch (Exception ex)
            {
                return ServiceResult<LanguageGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a language by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Language? nulLanguage = null;
                var language = await _languageData.SelectForEntity(id);
                if (language == nulLanguage)
                {
                    return ServiceResult<bool>.FailureResult("Dil verisi bulunmuyor.");
                }
                _languageMapper.DeleteEntity(language);
                await _languageData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
