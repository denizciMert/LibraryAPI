using LibraryAPI.BLL.Core;
using LibraryAPI.Entities.DTOs.CityDTO;

namespace LibraryAPI.BLL.Interfaces
{
    public interface ILibraryServiceManager<TG, in TP,TD>
    {
        Task<ServiceResult<IEnumerable<TG>>> GetAllAsync();
        Task<ServiceResult<IEnumerable<TD>>> GetAllWithDataAsync();
        Task<ServiceResult<TG>> GetByIdAsync(int id);
        Task<ServiceResult<TD>> GetWithDataByIdAsync(int id);
        Task<ServiceResult<TG>> AddAsync(TP tPost);
        Task<ServiceResult<TG>> UpdateAsync(int id, TP tPost);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}