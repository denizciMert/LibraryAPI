using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.LocationDTO; // Importing the DTOs for Location
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Location-related data operations
    public class LocationData(ApplicationDbContext context) : IQueryBase<Location>
    {
        // Selecting all locations with filters applied asynchronously
        public async Task<List<Location>> SelectAllFiltered()
        {
            return await context.Locations!
                .Where(x => x.State != State.Silindi) // Filtering out locations with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all locations asynchronously
        public async Task<List<Location>> SelectAll()
        {
            return await context.Locations!.ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a location by ID asynchronously
        public async Task<Location> SelectForEntity(int id)
        {
            return (await context.Locations!.FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first location with the specified ID
        }

        // Not implemented for this entity
        public Task<Location> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a location is already registered asynchronously
        public async Task<bool> IsRegistered(LocationPost tPost)
        {
            var locations = await SelectAllFiltered(); // Getting all filtered locations
            foreach (var location in locations)
            {
                if (location.ShelfCode == tPost.ShelfCode) // Checking if a location is registered with the same ShelfCode
                {
                    return true; // Location is already registered
                }
            }
            return false; // Location is not registered
        }

        // Adding a location to the context
        public void AddToContext(Location location)
        {
            context.Locations!.Add(location); // Adding the location to the Locations DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
