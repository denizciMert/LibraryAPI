using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO; // Importing the DTOs for PenaltyType
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for PenaltyType-related data operations
    public class PenaltyTypeData(ApplicationDbContext context) : IQueryBase<PenaltyType>
    {
        // Selecting all penalty types with filters applied asynchronously
        public async Task<List<PenaltyType>> SelectAllFiltered()
        {
            return await context.PenaltyTypes!
                .Where(x => x.State != State.Silindi) // Filtering out penalty types with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all penalty types asynchronously
        public async Task<List<PenaltyType>> SelectAll()
        {
            return await context.PenaltyTypes!.ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a penalty type by ID asynchronously
        public async Task<PenaltyType> SelectForEntity(int id)
        {
            return (await context.PenaltyTypes!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first penalty type with the specified ID
        }

        // Not implemented for this entity
        public Task<PenaltyType> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a penalty type is already registered asynchronously
        public async Task<bool> IsRegistered(PenaltyTypePost tPost)
        {
            var penaltyTypes = await SelectAllFiltered(); // Getting all filtered penalty types
            foreach (var penaltyType in penaltyTypes)
            {
                if (penaltyType.PenaltyName == tPost.PenaltyType &&
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    penaltyType.AmountToPay == tPost.AmountToPay) // Checking if a penalty type is registered with the same name and amount to pay
                {
                    return true; // Penalty type is already registered
                }
            }
            return false; // Penalty type is not registered
        }

        // Adding a penalty type to the context
        public void AddToContext(PenaltyType penaltyType)
        {
            context.PenaltyTypes!.Add(penaltyType); // Adding the penalty type to the PenaltyTypes DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
