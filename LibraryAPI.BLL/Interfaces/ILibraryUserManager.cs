using LibraryAPI.BLL.Core;

namespace LibraryAPI.BLL.Interfaces
{
    public interface ILibraryUserManager<TG, in TP, TD>
    {
        Task<ServiceResult<IEnumerable<TG>>> GetAllAsync();
        Task<ServiceResult<IEnumerable<TD>>> GetAllWithDataAsync();
        Task<ServiceResult<TG>> GetByIdAsync(string id);
        Task<ServiceResult<TD>> GetWithDataByIdAsync(string id);
        Task<ServiceResult<TG>> AddAsync(TP tPost);
        Task<ServiceResult<TG>> UpdateAsync(string id, TP tPost);
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }
}
