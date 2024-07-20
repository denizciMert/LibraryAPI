using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.LocationDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class LocationService : ILibraryServiceManager<LocationGet,LocationPost,Location>
    {
        private readonly LocationData _locationData;
        private readonly LocationMapper _locationMapper;

        public LocationService(ApplicationDbContext context)
        {
            _locationData = new LocationData(context);
            _locationMapper = new LocationMapper();
        }

        public async Task<ServiceResult<IEnumerable<LocationGet>>> GetAllAsync()
        {
            try
            {
                var locations = await _locationData.SelectAll();
                if (locations == null || locations.Count == 0)
                {
                    return ServiceResult<IEnumerable<LocationGet>>.FailureResult("Konum verisi bulunmuyor.");
                }
                List<LocationGet> locationGets = new List<LocationGet>();
                foreach (var location in locations)
                {
                    var locationGet = _locationMapper.MapToDto(location);
                    locationGets.Add(locationGet);
                }
                return ServiceResult<IEnumerable<LocationGet>>.SuccessResult(locationGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<LocationGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Location>>> GetAllWithDataAsync()
        {
            try
            {
                var locations = await _locationData.SelectAll();
                if (locations == null || locations.Count == 0)
                {
                    return ServiceResult<IEnumerable<Location>>.FailureResult("Konum verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Location>>.SuccessResult(locations);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Location>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<LocationGet>> GetByIdAsync(int id)
        {
            try
            {
                var location = await _locationData.SelectForEntity(id);
                if (location == null)
                {
                    return ServiceResult<LocationGet>.FailureResult("Konum verisi bulunmuyor.");
                }
                var locationGet = _locationMapper.MapToDto(location);
                return ServiceResult<LocationGet>.SuccessResult(locationGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<LocationGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Location>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var location = await _locationData.SelectForEntity(id);
                if (location == null)
                {
                    return ServiceResult<Location>.FailureResult("Konum verisi bulunmuyor.");
                }
                return ServiceResult<Location>.SuccessResult(location);
            }
            catch (Exception ex)
            {
                return ServiceResult<Location>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<LocationGet>> AddAsync(LocationPost tPost)
        {
            try
            {
                if (await _locationData.IsRegistered(tPost))
                {
                    return ServiceResult<LocationGet>.FailureResult("Bu konum zaten eklenmiş.");
                }
                var newLocation = _locationMapper.PostEntity(tPost);
                _locationData.AddToContext(newLocation);
                await _locationData.SaveContext();
                var result = await GetByIdAsync(newLocation.Id);
                return ServiceResult<LocationGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<LocationGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<LocationGet>> UpdateAsync(int id, LocationPost tPost)
        {
            try
            {
                var location = await _locationData.SelectForEntity(id);
                if (location == null)
                {
                    return ServiceResult<LocationGet>.FailureResult("Konum verisi bulunmuyor.");
                }
                _locationMapper.UpdateEntity(location, tPost);
                await _locationData.SaveContext();
                var updatedLocation = _locationMapper.MapToDto(location);
                return ServiceResult<LocationGet>.SuccessResult(updatedLocation);
            }
            catch (Exception ex)
            {
                return ServiceResult<LocationGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var location = await _locationData.SelectForEntity(id);
                if (location == null)
                {
                    return ServiceResult<bool>.FailureResult("Konum verisi bulunmuyor.");
                }
                _locationMapper.DeleteEntity(location);
                await _locationData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
