using LibraryAPI.BLL.Core; // Importing the core functionalities of the BLL

namespace LibraryAPI.BLL.Interfaces
{
    // Interface for a generic library user manager
    public interface ILibraryUserManager<TG, in TP, TD>
    {
        // Method to get all users asynchronously
        Task<ServiceResult<IEnumerable<TG>>> GetAllAsync();

        // Method to get all users with additional data asynchronously
        Task<ServiceResult<IEnumerable<TD>>> GetAllWithDataAsync();

        // Method to get a user by ID asynchronously
        Task<ServiceResult<TG>> GetByIdAsync(string id);

        // Method to get a user with additional data by ID asynchronously
        Task<ServiceResult<TD>> GetWithDataByIdAsync(string id);

        // Method to add a new user asynchronously
        Task<ServiceResult<TG>> AddAsync(TP tPost);

        // Method to update an existing user asynchronously
        Task<ServiceResult<TG>> UpdateAsync(string id, TP tPost);

        // Method to delete a user asynchronously
        Task<ServiceResult<bool>> DeleteAsync(string id);
    }
}