using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.DistrictDTO; // Importing the DTOs for District
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for District-related data operations
    public class DistrictData(ApplicationDbContext context) : IQueryBase<District>
    {
        // Selecting all districts with filters applied asynchronously
        public async Task<List<District>> SelectAllFiltered()
        {
            return await context.Districts!
                .Include(x => x.City) // Including related City entity
                .ThenInclude(x => x!.Country) // Including related Country entity
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted districts
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all districts asynchronously
        public async Task<List<District>> SelectAll()
        {
            return await context.Districts!
                .Include(x => x.City) // Including related City entity
                .ThenInclude(x => x!.Country) // Including related Country entity
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting a district by its ID asynchronously
        public async Task<District> SelectForEntity(int id)
        {
            return (await context.Districts!
                .Include(x => x.City) // Including related City entity
                .ThenInclude(x => x!.Country) // Including related Country entity
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the district by ID
        }

        // Not implemented method for selecting a district by user ID
        public Task<District> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Checking if a district is already registered asynchronously
        public async Task<bool> IsRegistered(DistrictPost tPost)
        {
            var districts = await SelectAllFiltered(); // Selecting all filtered districts
            foreach (var district in districts) // Iterating through each district
            {
                if (district.CityId == tPost.CityId &&
                    district.DistrictName == tPost.District) // Checking if the city ID and district name match
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding a district to the context
        public void AddToContext(District district)
        {
            context.Districts!.Add(district); // Adding the district to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
