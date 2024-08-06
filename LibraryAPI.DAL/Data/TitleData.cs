using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.TitleDTO; // Importing the DTOs for Title
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Title-related data operations
    public class TitleData(ApplicationDbContext context) : IQueryBase<Title>
    {
        // Selecting all titles with filters applied asynchronously
        public async Task<List<Title>> SelectAllFiltered()
        {
            return await context.Titles!
                .Where(x => x.State != State.Silindi) // Filtering out titles with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all titles asynchronously
        public async Task<List<Title>> SelectAll()
        {
            return await context.Titles!.ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a title by ID asynchronously
        public async Task<Title> SelectForEntity(int id)
        {
            return (await context.Titles!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first title with the specified ID
        }

        // Not implemented for this entity
        public Task<Title> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a title is already registered asynchronously
        public async Task<bool> IsRegistered(TitlePost tPost)
        {
            var titles = await SelectAllFiltered(); // Getting all filtered titles
            foreach (var title in titles)
            {
                if (title.TitleName == tPost.TitleName) // Checking if a title is registered with the same name
                {
                    return true; // Title is already registered
                }
            }
            return false; // Title is not registered
        }

        // Adding a title to the context
        public void AddToContext(Title title)
        {
            context.Titles!.Add(title); // Adding the title to the Titles DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
