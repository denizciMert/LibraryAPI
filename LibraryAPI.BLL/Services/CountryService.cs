using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.CountryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class CountryService : ILibraryServiceManager<CountryGet, CountryPost, Country>
    {
        private readonly ApplicationDbContext _context;
        private readonly CountryData _countryData;
        private readonly CountryMapper _countryMapper;

        public CountryService(ApplicationDbContext context)
        {
            _context = context;
            _countryData = new CountryData(_context);
            _countryMapper = new CountryMapper();
        }

        public async Task<ServiceResult<IEnumerable<CountryGet>>> GetAllAsync()
        {
            try
            {
                var countries = await _countryData.SelectAll();
                if (countries == null || countries.Count == 0)
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
                return ServiceResult<IEnumerable<CountryGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Country>>> GetAllWithDataAsync()
        {
            try
            {
                var countries = await _countryData.SelectAll();
                if (countries == null || countries.Count == 0)
                {
                    return ServiceResult<IEnumerable<Country>>.FailureResult("Ülke verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Country>>.SuccessResult(countries);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Country>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CountryGet>> GetByIdAsync(int id)
        {
            try
            {
                var country = await _countryData.SelectForEntity(id);
                if (country == null)
                {
                    return ServiceResult<CountryGet>.FailureResult("Ülke verisi bulunmuyor.");
                }
                var countryGet = _countryMapper.MapToDto(country);
                return ServiceResult<CountryGet>.SuccessResult(countryGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<CountryGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Country>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var country = await _countryData.SelectForEntity(id);
                if (country == null)
                {
                    return ServiceResult<Country>.FailureResult("Ülke verisi bulunmuyor.");
                }
                return ServiceResult<Country>.SuccessResult(country);
            }
            catch (Exception ex)
            {
                return ServiceResult<Country>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

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
                return ServiceResult<CountryGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<CountryGet>> UpdateAsync(int id, CountryPost tPost)
        {
            try
            {
                var country = await _countryData.SelectForEntity(id);
                if (country == null)
                {
                    return ServiceResult<CountryGet>.FailureResult("Ülke verisi bulunmuyor.");
                }
                _countryMapper.UpdateEntity(country, tPost);
                await _countryData.SaveContext();
                var newCountry = _countryMapper.MapToDto(country);
                return ServiceResult<CountryGet>.SuccessResult(newCountry);
            }
            catch (Exception ex)
            {
                return ServiceResult<CountryGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var country = await _countryData.SelectForEntity(id);
                if (country == null)
                {
                    return ServiceResult<bool>.FailureResult("Ülke verisi bulunmuyor.");
                }
                _countryMapper.DeleteEntity(country);
                await _countryData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
