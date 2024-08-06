using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// CityService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to city management.
    /// </summary>
    public class CityService : ILibraryServiceManager<CityGet, CityPost, City>
    {
        // Private fields to hold instances of data and mappers.
        private readonly CityData _cityData;
        private readonly CityMapper _cityMapper;

        /// <summary>
        /// Constructor to initialize the CityService with necessary dependencies.
        /// </summary>
        public CityService(ApplicationDbContext context)
        {
            _cityData = new CityData(context);
            _cityMapper = new CityMapper();
        }

        /// <summary>
        /// Retrieves all cities.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<CityGet>>> GetAllAsync()
        {
            try
            {
                var cities = await _cityData.SelectAllFiltered();
                if (cities.Count == 0)
                {
                    return ServiceResult<IEnumerable<CityGet>>.FailureResult("Şehir verisi bulunmuyor.");
                }
                List<CityGet> cityGets = new List<CityGet>();
                foreach (var city in cities)
                {
                    var cityGet = _cityMapper.MapToDto(city);
                    cityGets.Add(cityGet);
                }
                return ServiceResult<IEnumerable<CityGet>>.SuccessResult(cityGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<CityGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all cities with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<City>>> GetAllWithDataAsync()
        {
            try
            {
                var cities = await _cityData.SelectAll();
                if (cities.Count == 0)
                {
                    return ServiceResult<IEnumerable<City>>.FailureResult("Şehir verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<City>>.SuccessResult(cities);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<City>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a city by its ID.
        /// </summary>
        public async Task<ServiceResult<CityGet>> GetByIdAsync(int id)
        {
            try
            {
                City? nullCity = null;
                var city = await _cityData.SelectForEntity(id);
                if (city == nullCity)
                {
                    return ServiceResult<CityGet>.FailureResult("Şehir verisi bulunmuyor.");
                }
                var cityGet = _cityMapper.MapToDto(city);
                return ServiceResult<CityGet>.SuccessResult(cityGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CityGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves a city with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<City>> GetWithDataByIdAsync(int id)
        {
            try
            {
                City? nullCity = null;
                var city = await _cityData.SelectForEntity(id);
                if (city == nullCity)
                {
                    return ServiceResult<City>.FailureResult("Şehir verisi bulunmuyor.");
                }
                return ServiceResult<City>.SuccessResult(city);
            }
            catch (Exception ex)
            {
                return ServiceResult<City>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new city.
        /// </summary>
        public async Task<ServiceResult<CityGet>> AddAsync(CityPost tPost)
        {
            try
            {
                if (await _cityData.IsRegistered(tPost))
                {
                    return ServiceResult<CityGet>.FailureResult("Bu şehir zaten eklenmiş.");
                }
                var newCity = _cityMapper.PostEntity(tPost);
                _cityData.AddToContext(newCity);
                await _cityData.SaveContext();
                var result = await GetByIdAsync(newCity.Id);
                return ServiceResult<CityGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<CityGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing city.
        /// </summary>
        public async Task<ServiceResult<CityGet>> UpdateAsync(int id, CityPost tPost)
        {
            try
            {
                City? nullCity = null;
                var city = await _cityData.SelectForEntity(id);
                if (city == nullCity)
                {
                    return ServiceResult<CityGet>.FailureResult("Şehir verisi bulunmuyor.");
                }
                if (await _cityData.IsRegistered(tPost))
                {
                    return ServiceResult<CityGet>.FailureResult("Bu şehir zaten eklenmiş.");
                }
                _cityMapper.UpdateEntity(city, tPost);
                await _cityData.SaveContext();
                var newCity = _cityMapper.MapToDto(city);
                return ServiceResult<CityGet>.SuccessResult(newCity);
            }
            catch (Exception ex)
            {
                return ServiceResult<CityGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes a city by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                City? nullCity = null;
                var city = await _cityData.SelectForEntity(id);
                if (city == nullCity)
                {
                    return ServiceResult<bool>.FailureResult("Şehir verisi bulunmuyor.");
                }
                _cityMapper.DeleteEntity(city);
                await _cityData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
