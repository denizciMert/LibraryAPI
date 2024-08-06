using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.CountryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// CountryService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to country management.
    /// </summary>
    public class CountryService : ILibraryServiceManager<CountryGet, CountryPost, Country>
    {
        // Private fields to hold instances of data and mappers.
        private readonly CountryData _countryData;
        private readonly CountryMapper _countryMapper;

        /// <summary>
        /// Constructor to initialize the CountryService with necessary dependencies.
        /// </summary>
        public CountryService(ApplicationDbContext context)
        {
            _countryData = new CountryData(context);
            _countryMapper = new CountryMapper();
        }

        /// <summary>
        /// Retrieves all countries.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<CountryGet>>> GetAllAsync()
        {
            try
            {
                var countries = await _countryData.SelectAllFiltered();
                if (countries.Count == 0)
                {
                    return ServiceResult<IEnumerable<CountryGet>>.FailureResult("Ülke verisi bulunmuyor.");
                }
                List<CountryGet> countryGets = new List<CountryGet>();
                foreach (var country in countries)
                {
                    var countryGet = _countryMapper.MapToDto(country);
                    countryGets.Add(countryGet);
                }
                return ServiceResult<IEnumerable<CountryGet>>.SuccessResult(countryGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<CountryGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all countries with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Country>>> GetAllWithDataAsync()
        {
            try
            {
                var countries = await _countryData.SelectAll();
                if (countries.Count == 0)
                {
                    return ServiceResult<IEnumerable<Country>>.FailureResult("Ülke verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Country>>.SuccessResult(countries);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Country>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a country by its ID.
        /// </summary>
        public async Task<ServiceResult<CountryGet>> GetByIdAsync(int id)
        {
            try
            {
                Country? nullCountry = null;
                var country = await _countryData.SelectForEntity(id);
                if (country == nullCountry)
                {
                    return ServiceResult<CountryGet>.FailureResult("Ülke verisi bulunmuyor.");
                }
                var countryGet = _countryMapper.MapToDto(country);
                return ServiceResult<CountryGet>.SuccessResult(countryGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CountryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a country with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Country>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Country? nullCountry = null;
                var country = await _countryData.SelectForEntity(id);
                if (country == nullCountry)
                {
                    return ServiceResult<Country>.FailureResult("Ülke verisi bulunmuyor.");
                }
                return ServiceResult<Country>.SuccessResult(country);
            }
            catch (Exception ex)
            {
                return ServiceResult<Country>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new country.
        /// </summary>
        public async Task<ServiceResult<CountryGet>> AddAsync(CountryPost tPost)
        {
            try
            {
                if (await _countryData.IsRegistered(tPost))
                {
                    return ServiceResult<CountryGet>.FailureResult("Bu ülke zaten eklenmiş.");
                }
                var newCountry = _countryMapper.PostEntity(tPost);
                _countryData.AddToContext(newCountry);
                await _countryData.SaveContext();
                var result = await GetByIdAsync(newCountry.Id);
                return ServiceResult<CountryGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<CountryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing country.
        /// </summary>
        public async Task<ServiceResult<CountryGet>> UpdateAsync(int id, CountryPost tPost)
        {
            try
            {
                Country? nullCountry = null;
                var country = await _countryData.SelectForEntity(id);
                if (country == nullCountry)
                {
                    return ServiceResult<CountryGet>.FailureResult("Ülke verisi bulunmuyor.");
                }
                if (await _countryData.IsRegistered(tPost))
                {
                    return ServiceResult<CountryGet>.FailureResult("Bu ülke zaten eklenmiş.");
                }
                _countryMapper.UpdateEntity(country, tPost);
                await _countryData.SaveContext();
                var newCountry = _countryMapper.MapToDto(country);
                return ServiceResult<CountryGet>.SuccessResult(newCountry);
            }
            catch (Exception ex)
            {
                return ServiceResult<CountryGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a country by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Country? nullCountry = null;
                var country = await _countryData.SelectForEntity(id);
                if (country == nullCountry)
                {
                    return ServiceResult<bool>.FailureResult("Ülke verisi bulunmuyor.");
                }
                _countryMapper.DeleteEntity(country);
                await _countryData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
