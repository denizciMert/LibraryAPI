namespace LibraryAPI.DAL.Data.Interfaces
{
    public interface IQueryBase<T>
    {
        Task<List<T>> SelectAll();
        Task<T> SelectForEntity(int id);
        Task<T> SelectForUser(string id);
    }
}
