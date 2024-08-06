using LibraryAPI.BLL.Core; // Importing the core functionalities of the BLL

namespace LibraryAPI.BLL.Interfaces
{
    // Interface for a generic library service manager
    public interface ILibraryServiceManager<TG, in TP, TD>
    {
        // Method to get all entities asynchronously
        Task<ServiceResult<IEnumerable<TG>>> GetAllAsync();

        // Method to get all entities with additional data asynchronously
        Task<ServiceResult<IEnumerable<TD>>> GetAllWithDataAsync();

        // Method to get an entity by ID asynchronously
        Task<ServiceResult<TG>> GetByIdAsync(int id);

        // Method to get an entity with additional data by ID asynchronously
        Task<ServiceResult<TD>> GetWithDataByIdAsync(int id);

        // Method to add a new entity asynchronously
        Task<ServiceResult<TG>> AddAsync(TP tPost);

        // Method to update an existing entity asynchronously
        Task<ServiceResult<TG>> UpdateAsync(int id, TP tPost);

        // Method to delete an entity asynchronously
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}