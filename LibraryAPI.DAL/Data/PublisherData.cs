using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.PublisherDTO; // Importing the DTOs for Publisher
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Publisher-related data operations
    public class PublisherData(ApplicationDbContext context) : IQueryBase<Publisher>
    {
        // Selecting all publishers with filters applied asynchronously
        public async Task<List<Publisher>> SelectAllFiltered()
        {
            return await context.Publishers!
                .Where(x => x.State != State.Silindi) // Filtering out publishers with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all publishers asynchronously
        public async Task<List<Publisher>> SelectAll()
        {
            return await context.Publishers!.ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a publisher by ID asynchronously
        public async Task<Publisher> SelectForEntity(int id)
        {
            return (await context.Publishers!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first publisher with the specified ID
        }

        // Not implemented for this entity
        public Task<Publisher> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a publisher is already registered asynchronously
        public async Task<bool> IsRegistered(PublisherPost tPost)
        {
            var publishers = await SelectAllFiltered(); // Getting all filtered publishers
            foreach (var publisher in publishers)
            {
                if (publisher.PublisherName == tPost.PublisherName) // Checking if a publisher is registered with the same name
                {
                    return true; // Publisher is already registered
                }
            }
            return false; // Publisher is not registered
        }

        // Adding a publisher to the context
        public void AddToContext(Publisher publisher)
        {
            context.Publishers!.Add(publisher); // Adding the publisher to the Publishers DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
