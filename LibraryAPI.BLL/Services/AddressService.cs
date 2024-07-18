using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class AddressService : ILibraryServiceManager<AddressGet, AddressPost, Address>
    {
        private readonly AddressData _addressData;

        public AddressService(AddressData addressData)
        {
            _addressData = addressData;
        }

        public async Task<ServiceResult<IEnumerable<AddressGet>>> GetAllAsync()
        {
            try
            {
                var addresses = await _addressData.SelectAll();

                if (addresses == null || addresses.Count == 0)
                {
                    return ServiceResult<IEnumerable<AddressGet>>.FailureResult("Adres verisi bulunmuyor.");
                }
                List<AddressGet> addressGets = new List<AddressGet>();
                foreach (var address in addresses)
                {
                    var addressGet = new AddressGet
                    {
                        Id = address.Id,
                        UserName = address.ApplicationUser.UserName,
                        UserFullName = $"{ address.ApplicationUser.FirstName } { address.ApplicationUser.LastName }",
                        AddressString = address.AddressString,
                        District = address.District.DistrictName,
                        City = address.District.City.CityName,
                        Country = address.District.City.Country.CountryName,
                        State = address.State.ToString(),
                        CreatinDateLog = address.CreationDateLog,
                        UpdateDateLog = address.UpdateDateLog,
                        DeleteDateLog = address.DeleteDateLog
                    };
                    addressGets.Add(addressGet);
                }
                return ServiceResult<IEnumerable<AddressGet>>.SuccessResult(addressGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<AddressGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<Address>>> GetAllWithDataAsync()
        {
            try
            {
                var addresses = await _addressData.SelectAll();

                if (addresses == null || addresses.Count == 0)
                {
                    return ServiceResult<IEnumerable<Address>>.FailureResult("Adres verisi bulunmuyor.");
                }

                return ServiceResult<IEnumerable<Address>>.SuccessResult(addresses);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Address>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AddressGet>> GetByIdAsync(int id)
        {
            try
            {
                var address = await _addressData.SelectForEntity(id);

                if (address == null)
                {
                    return ServiceResult<AddressGet>.FailureResult("Adres verisi bulunmuyor.");
                }

                var addressGet = new AddressGet
                {
                    Id = address.Id,
                    UserName = address.ApplicationUser.UserName,
                    UserFullName = $"{address.ApplicationUser.FirstName} {address.ApplicationUser.LastName}",
                    AddressString = address.AddressString,
                    District = address.District.DistrictName,
                    City = address.District.City.CityName,
                    Country = address.District.City.Country.CountryName,
                    State = address.State.ToString(),
                    CreatinDateLog = address.CreationDateLog,
                    UpdateDateLog = address.UpdateDateLog,
                    DeleteDateLog = address.DeleteDateLog
                };

                return ServiceResult<AddressGet>.SuccessResult(addressGet);
            }
            catch (Exception ex)
            {
                return ServiceResult<AddressGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<Address>> GetWithDataByIdAsync(int id)
        {
            try
            {
                var address = await _addressData.SelectForEntity(id);

                if (address == null)
                {
                    return ServiceResult<Address>.FailureResult("Adres verisi bulunmuyor.");
                }

                return ServiceResult<Address>.SuccessResult(address);
            }
            catch (Exception ex)
            {
                return ServiceResult<Address>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AddressGet>> AddAsync(AddressPost tPost)
        {
            try
            {
                if (await _addressData.IsRegistered(tPost))
                {
                    return ServiceResult<AddressGet>.FailureResult("Bu adres zaten eklenmiş.");
                }

                var newAddress = new Address
                {
                    UserId = tPost.UserId,
                    DistrictId = tPost.DistrictId,
                    AddressString = tPost.AddressString,
                    State = Entities.Enums.State.Eklendi,
                    CreationDateLog = DateTime.Now,
                    DeleteDateLog = null,
                    UpdateDateLog = null
                };
                _addressData.AddToContext(newAddress);
                await _addressData.SaveContext();

                var result = await GetByIdAsync(newAddress.Id);

                return ServiceResult<AddressGet>.SuccessResult(result.Data);
            }
            catch (Exception ex)
            {
                return ServiceResult<AddressGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<AddressGet>> UpdateAsync(int id, AddressPost tPost)
        {
            try
            {
                var address = await _addressData.SelectForEntity(id);

                if (address == null)
                {
                    return ServiceResult<AddressGet>.FailureResult("Adres verisi bulunmuyor.");
                }

                address.AddressString=tPost.AddressString;
                address.DistrictId =tPost.DistrictId;
                address.State = Entities.Enums.State.Güncellendi;
                address.UpdateDateLog = DateTime.Now;
                await _addressData.SaveContext();

                var newAddress = new AddressGet
                {
                    Id = address.Id,
                    UserName = address.ApplicationUser.UserName,
                    UserFullName = $"{address.ApplicationUser.FirstName} {address.ApplicationUser.LastName}",
                    AddressString = address.AddressString,
                    District = address.District.DistrictName,
                    City = address.District.City.CityName,
                    Country = address.District.City.Country.CountryName,
                    State = address.State.ToString(),
                    CreatinDateLog = address.CreationDateLog,
                    UpdateDateLog = address.UpdateDateLog,
                    DeleteDateLog = address.DeleteDateLog
                };

                return ServiceResult<AddressGet>.SuccessResult(newAddress);
            }
            catch (Exception ex)
            {
                return ServiceResult<AddressGet>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var address = await _addressData.SelectForEntity(id);

                if (address == null)
                {
                    return ServiceResult<bool>.FailureResult("Kategori verisi bulunmuyor.");
                }

                address.DeleteDateLog = DateTime.Now;
                address.State = Entities.Enums.State.Silindi;
                await _addressData.SaveContext();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
