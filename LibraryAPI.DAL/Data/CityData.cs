using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.CityDTO; // Importing the DTOs for City
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for City-related data operations
    public class CityData(ApplicationDbContext context) : IQueryBase<City>
    {
        // Selecting all cities with filters applied asynchronously
        public async Task<List<City>> SelectAllFiltered()
        {
            return await context.Cities!
                .Include(x => x.Country) // Including related Country
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted cities
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all cities asynchronously
        public async Task<List<City>> SelectAll()
        {
            return await context.Cities!
                .Include(x => x.Country) // Including related Country
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting a city by its ID asynchronously
        public async Task<City> SelectForEntity(int id)
        {
            return (await context.Cities!
                .Include(x => x.Country) // Including related Country
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the city by ID
        }

        // Not implemented method for selecting a city by user ID
        public Task<City> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Checking if a city is already registered asynchronously
        public async Task<bool> IsRegistered(CityPost tPost)
        {
            var cities = await SelectAllFiltered(); // Selecting all filtered cities
            foreach (var city in cities) // Iterating through each city
            {
                if (city.CityName == tPost.CityName &&
                    city.CountryId == tPost.CountryId) // Checking if the city name and country ID match
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding a city to the context
        public void AddToContext(City city)
        {
            context.Cities!.Add(city); // Adding the city to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
