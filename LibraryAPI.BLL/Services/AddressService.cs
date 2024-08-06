using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// AddressService class implements the ILibraryServiceManager interface and provides
    /// functionalities related to address management.
    /// </summary>
    public class AddressService : ILibraryServiceManager<AddressGet, AddressPost, Address>
    {
        // Private fields to hold instances of data and mappers.
        private readonly AddressData _addressData;
        private readonly AddressMapper _addressMapper;

        /// <summary>
        /// Constructor to initialize the AddressService with necessary dependencies.
        /// </summary>
        public AddressService(ApplicationDbContext context)
        {
            _addressData = new AddressData(context);
            _addressMapper = new AddressMapper();
        }

        /// <summary>
        /// Retrieves all addresses.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<AddressGet>>> GetAllAsync()
        {
            try
            {
                var addresses = await _addressData.SelectAllFiltered();
                if (addresses.Count == 0)
                {
                    return ServiceResult<IEnumerable<AddressGet>>.FailureResult("Adres verisi bulunmuyor.");
                }
                List<AddressGet> addressGets = new List<AddressGet>();
                foreach (var address in addresses)
                {
                    var addressGet = _addressMapper.MapToDto(address);
                    addressGets.Add(addressGet);
                }
                return ServiceResult<IEnumerable<AddressGet>>.SuccessResult(addressGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<AddressGet>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves all addresses with detailed data.
        /// </summary>
        public async Task<ServiceResult<IEnumerable<Address>>> GetAllWithDataAsync()
        {
            try
            {
                var addresses = await _addressData.SelectAll();
                if (addresses.Count == 0)
                {
                    return ServiceResult<IEnumerable<Address>>.FailureResult("Adres verisi bulunmuyor.");
                }
                return ServiceResult<IEnumerable<Address>>.SuccessResult(addresses);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Address>>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves an address by its ID.
        /// </summary>
        public async Task<ServiceResult<AddressGet>> GetByIdAsync(int id)
        {
            try
            {
                Address? nullAddress = null;
                var address = await _addressData.SelectForEntity(id);
                if (address == nullAddress)
                {
                    return ServiceResult<AddressGet>.FailureResult("Adres verisi bulunmuyor.");
                }
                var addressGet = _addressMapper.MapToDto(address);
                return ServiceResult<AddressGet>.SuccessResult(addressGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<AddressGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Retrieves an address with detailed data by its ID.
        /// </summary>
        public async Task<ServiceResult<Address>> GetWithDataByIdAsync(int id)
        {
            try
            {
                Address? nullAddress = null;
                var address = await _addressData.SelectForEntity(id);
                if (address == nullAddress)
                {
                    return ServiceResult<Address>.FailureResult("Adres verisi bulunmuyor.");
                }
                return ServiceResult<Address>.SuccessResult(address);
            }
            catch (Exception ex)
            {
                return ServiceResult<Address>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Adds a new address.
        /// </summary>
        public async Task<ServiceResult<AddressGet>> AddAsync(AddressPost tPost)
        {
            try
            {
                if (await _addressData.IsRegistered(tPost))
                {
                    return ServiceResult<AddressGet>.FailureResult("Bu adres zaten eklenmiş.");
                }
                var newAddress = _addressMapper.PostEntity(tPost);
                _addressData.AddToContext(newAddress);
                await _addressData.SaveContext();
                var result = await GetByIdAsync(newAddress.Id);
                return ServiceResult<AddressGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<AddressGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        public async Task<ServiceResult<AddressGet>> UpdateAsync(int id, AddressPost tPost)
        {
            try
            {
                Address? nullAddress = null;
                var address = await _addressData.SelectForEntity(id);
                if (address == nullAddress)
                {
                    return ServiceResult<AddressGet>.FailureResult("Adres verisi bulunmuyor.");
                }
                if (await _addressData.IsRegistered(tPost))
                {
                    return ServiceResult<AddressGet>.FailureResult("Bu adres zaten eklenmiş.");
                }
                _addressMapper.UpdateEntity(address, tPost);
                await _addressData.SaveContext();
                var newAddress = _addressMapper.MapToDto(address);
                return ServiceResult<AddressGet>.SuccessResult(newAddress);
            }
            catch (Exception ex)
            {
                return ServiceResult<AddressGet>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Deletes an address by its ID.
        /// </summary>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                Address? nullAddress = null;
                var address = await _addressData.SelectForEntity(id);
                if (address == nullAddress)
                {
                    return ServiceResult<bool>.FailureResult("Kategori verisi bulunmuyor.");
                }
                _addressMapper.DeleteEntity(address);
                await _addressData.SaveContext();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
