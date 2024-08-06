using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.CountryDTO; // Importing the DTOs for Country
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Country-related data operations
    public class CountryData(ApplicationDbContext context) : IQueryBase<Country>
    {
        // Selecting all countries with filters applied asynchronously
        public async Task<List<Country>> SelectAllFiltered()
        {
            return await context.Countries!
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted countries
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all countries asynchronously
        public async Task<List<Country>> SelectAll()
        {
            return await context.Countries!
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting a country by its ID asynchronously
        public async Task<Country> SelectForEntity(int id)
        {
            return (await context.Countries!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the country by ID
        }

        // Not implemented method for selecting a country by user ID
        public Task<Country> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Checking if a country is already registered asynchronously
        public async Task<bool> IsRegistered(CountryPost tPost)
        {
            var countries = await SelectAllFiltered(); // Selecting all filtered countries
            foreach (var country in countries) // Iterating through each country
            {
                if (country.CountryName == tPost.CountryName) // Checking if the country name matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding a country to the context
        public void AddToContext(Country country)
        {
            context.Countries!.Add(country); // Adding the country to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
