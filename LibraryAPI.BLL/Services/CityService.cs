using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class CityService : ILibraryServiceManager<CityGet, CityPost, City>
    {
        private readonly CityData _cityData;
        private readonly CityMapper _cityMapper;

        public CityService(ApplicationDbContext context)
        {
            _cityData = new CityData(context);
            _cityMapper = new CityMapper();
        }

        public async Task<ServiceResult<IEnumerable<CityGet>>> GetAllAsync()
        {
            try
            {
                var cities = await _cityData.SelectAll();
                if (cities == null || cities.Count == 0)
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
                return ServiceResult<IEnumerable<CityGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<City>>> GetAllWithDataAsync()
        {
            try
            {
                var cities = await _cityData.SelectAll();
                if (cities == null || cities.Count == 0)
                {
                    return ServiceResult<IEnumerable<City>>.FailureResult("Şehir verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<City>>.SuccessResult(cities);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<City>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CityGet>> GetByIdAsync(int id)
        {
            try
            {
                var city = await _cityData.SelectForEntity(id);
                if (city == null)
                {
                    return ServiceResult<CityGet>.FailureResult("Şehir verisi bulunmuyor.");
                }
                var cityGet = _cityMapper.MapToDto(city);
                return ServiceResult<CityGet>.SuccessResult(cityGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CityGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<City>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var city = await _cityData.SelectForEntity(id);
                if (city == null)
                {
                    return ServiceResult<City>.FailureResult("Şehir verisi bulunmuyor.");
                }
                return ServiceResult<City>.SuccessResult(city);
            }
            catch (Exception ex)
            {
                return ServiceResult<City>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

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
                return ServiceResult<CityGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CityGet>> UpdateAsync(int id, CityPost tPost)
        {
            try
            {
                var city = await _cityData.SelectForEntity(id);
                if (city == null)
                {
                    return ServiceResult<CityGet>.FailureResult("Şehir verisi bulunmuyor.");
                }
                _cityMapper.UpdateEntity(city, tPost);
                await _cityData.SaveContext();
                var newCity = _cityMapper.MapToDto(city);
                return ServiceResult<CityGet>.SuccessResult(newCity);
            }
            catch (Exception ex)
            {
                return ServiceResult<CityGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var city = await _cityData.SelectForEntity(id);
                if (city == null)
                {
                    return ServiceResult<bool>.FailureResult("Şehir verisi bulunmuyor.");
                }
                _cityMapper.DeleteEntity(city);
                await _cityData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
