using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.PublisherDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class PublisherService : ILibraryServiceManager<PublisherGet,PublisherPost,Publisher>
    {
        private readonly PublisherData _publisherData;
        private readonly PublisherMapper _publisherMapper;

        public PublisherService(ApplicationDbContext context)
        {
            _publisherData = new PublisherData(context);
            _publisherMapper = new PublisherMapper();
        }

        public async Task<ServiceResult<IEnumerable<PublisherGet>>> GetAllAsync()
        {
            try
            {
                var publishers = await _publisherData.SelectAllFiltered();
                if (publishers == null || publishers.Count == 0)
                {
                    return ServiceResult<IEnumerable<PublisherGet>>.FailureResult("Yayıncı verisi bulunmuyor.");
                }
                List<PublisherGet> publisherGets = new List<PublisherGet>();
                foreach (var publisher in publishers)
                {
                    var publisherGet = _publisherMapper.MapToDto(publisher);
                    publisherGets.Add(publisherGet);
                }
                return ServiceResult<IEnumerable<PublisherGet>>.SuccessResult(publisherGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<PublisherGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Publisher>>> GetAllWithDataAsync()
        {
            try
            {
                var publishers = await _publisherData.SelectAll();
                if (publishers == null || publishers.Count == 0)
                {
                    return ServiceResult<IEnumerable<Publisher>>.FailureResult("Yayıncı verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Publisher>>.SuccessResult(publishers);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Publisher>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<PublisherGet>> GetByIdAsync(int id)
        {
            try
            {
                var publisher = await _publisherData.SelectForEntity(id);
                if (publisher == null)
                {
                    return ServiceResult<PublisherGet>.FailureResult("Yayıncı verisi bulunmuyor.");
                }
                var publisherGet = _publisherMapper.MapToDto(publisher);
                return ServiceResult<PublisherGet>.SuccessResult(publisherGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<PublisherGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Publisher>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var publisher = await _publisherData.SelectForEntity(id);
                if (publisher == null)
                {
                    return ServiceResult<Publisher>.FailureResult("Yayıncı verisi bulunmuyor.");
                }
                return ServiceResult<Publisher>.SuccessResult(publisher);
            }
            catch (Exception ex)
            {
                return ServiceResult<Publisher>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<PublisherGet>> AddAsync(PublisherPost tPost)
        {
            try
            {
                if (await _publisherData.IsRegistered(tPost))
                {
                    return ServiceResult<PublisherGet>.FailureResult("Bu yayıncı zaten eklenmiş.");
                }
                var newPublisher = _publisherMapper.PostEntity(tPost);
                _publisherData.AddToContext(newPublisher);
                await _publisherData.SaveContext();
                var result = await GetByIdAsync(newPublisher.Id);
                return ServiceResult<PublisherGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<PublisherGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<PublisherGet>> UpdateAsync(int id, PublisherPost tPost)
        {
            try
            {
                var publisher = await _publisherData.SelectForEntity(id);
                if (publisher == null)
                {
                    return ServiceResult<PublisherGet>.FailureResult("Yayıncı verisi bulunmuyor.");
                }
                _publisherMapper.UpdateEntity(publisher, tPost);
                await _publisherData.SaveContext();
                var updatedPublisher = _publisherMapper.MapToDto(publisher);
                return ServiceResult<PublisherGet>.SuccessResult(updatedPublisher);
            }
            catch (Exception ex)
            {
                return ServiceResult<PublisherGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var publisher = await _publisherData.SelectForEntity(id);
                if (publisher == null)
                {
                    return ServiceResult<bool>.FailureResult("Yayıncı verisi bulunmuyor.");
                }
                _publisherMapper.DeleteEntity(publisher);
                await _publisherData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
