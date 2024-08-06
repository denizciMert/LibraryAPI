using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.AddressDTO; // Importing the DTOs for Address
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Address-related data operations
    public class AddressData(ApplicationDbContext context) : IQueryBase<Address>
    {
        // Selecting all addresses with filters applied asynchronously
        public async Task<List<Address>> SelectAllFiltered()
        {
            return await context.Addresses!
                .Include(x => x.District) // Including related Districts
                .ThenInclude(x => x!.City) // Including related Cities
                .ThenInclude(x => x!.Country) // Including related Countries
                .Include(x => x.ApplicationUser) // Including related ApplicationUser
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted addresses
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all addresses asynchronously
        public async Task<List<Address>> SelectAll()
        {
            return await context.Addresses!
                .Include(x => x.District) // Including related Districts
                .ThenInclude(x => x!.City) // Including related Cities
                .ThenInclude(x => x!.Country) // Including related Countries
                .Include(x => x.ApplicationUser) // Including related ApplicationUser
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting an address by its ID asynchronously
        public async Task<Address> SelectForEntity(int id)
        {
            return (await context.Addresses!
                .Include(x => x.District) // Including related Districts
                .ThenInclude(x => x!.City) // Including related Cities
                .ThenInclude(x => x!.Country) // Including related Countries
                .Include(x => x.ApplicationUser) // Including related ApplicationUser
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the address by ID
        }

        // Selecting an address by user ID asynchronously
        public async Task<Address> SelectForUser(string id)
        {
            return (await context.Addresses!
                .Include(x => x.District) // Including related Districts
                .ThenInclude(x => x!.City) // Including related Cities
                .ThenInclude(x => x!.Country) // Including related Countries
                .Include(x => x.ApplicationUser) // Including related ApplicationUser
                .FirstOrDefaultAsync(x => x.UserId == id))!; // Finding the address by user ID
        }

        // Checking if an address is already registered asynchronously
        public async Task<bool> IsRegistered(AddressPost tPost)
        {
            var addresses = await SelectAllFiltered(); // Selecting all filtered addresses
            foreach (var address in addresses) // Iterating through each address
            {
                if (address.UserId == tPost.UserId && // Checking if the UserId matches
                    address.DistrictId == tPost.DistrictId && // Checking if the DistrictId matches
                    address.AddressString == tPost.AddressString) // Checking if the AddressString matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding an address to the context
        public void AddToContext(Address address)
        {
            context.Addresses!.Add(address); // Adding the address to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
