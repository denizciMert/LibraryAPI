namespace LibraryAPI.DAL.Data.Interfaces
{
    // Defining a generic interface for query operations
    public interface IQueryBase<T>
    {
        // Method to select all entities asynchronously
        Task<List<T>> SelectAll();

        // Method to select an entity by its ID asynchronously
        Task<T> SelectForEntity(int id);

        // Method to select an entity by a user ID asynchronously
        Task<T> SelectForUser(string id);
    }
}