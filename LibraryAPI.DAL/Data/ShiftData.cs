using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.ShiftDTO; // Importing the DTOs for Shift
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Shift-related data operations
    public class ShiftData(ApplicationDbContext context) : IQueryBase<Shift>
    {
        // Selecting all shifts with filters applied asynchronously
        public async Task<List<Shift>> SelectAllFiltered()
        {
            return await context.Shifts!
                .Where(x => x.State != State.Silindi) // Filtering out shifts with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all shifts asynchronously
        public async Task<List<Shift>> SelectAll()
        {
            return await context.Shifts!.ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a shift by ID asynchronously
        public async Task<Shift> SelectForEntity(int id)
        {
            return (await context.Shifts!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first shift with the specified ID
        }

        // Not implemented for this entity
        public Task<Shift> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a shift is already registered asynchronously
        public async Task<bool> IsRegistered(ShiftPost tPost)
        {
            var shifts = await SelectAllFiltered(); // Getting all filtered shifts
            foreach (var shift in shifts)
            {
                if (shift.ShiftType == tPost.ShiftType) // Checking if a shift is registered with the same shift type
                {
                    return true; // Shift is already registered
                }
            }
            return false; // Shift is not registered
        }

        // Adding a shift to the context
        public void AddToContext(Shift shift)
        {
            context.Shifts!.Add(shift); // Adding the shift to the Shifts DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
