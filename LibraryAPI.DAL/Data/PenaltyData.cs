using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.PenaltyDTO; // Importing the DTOs for Penalty
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.AspNetCore.Identity; // Importing the ASP.NET Core Identity functionalities
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Penalty-related data operations
    public class PenaltyData(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IQueryBase<Penalty>
    {
        // Selecting all penalties with filters applied asynchronously
        public async Task<List<Penalty>> SelectAllFiltered()
        {
            return await context.Penalties!
                .Include(x => x.Member)
                .ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x!.Employee)
                .ThenInclude(x => x!.ApplicationUser)
                .Where(x => x.Active == true) // Filtering only active penalties
                .Where(x => x.State != State.Silindi) // Filtering out penalties with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all penalties asynchronously
        public async Task<List<Penalty>> SelectAll()
        {
            return await context.Penalties!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x!.Employee).ThenInclude(x => x!.ApplicationUser)
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a penalty by ID asynchronously
        public async Task<Penalty> SelectForEntity(int id)
        {
            return (await context.Penalties!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x!.Employee).ThenInclude(x => x!.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first penalty with the specified ID
        }

        // Selecting a penalty by user ID asynchronously
        public async Task<Penalty> SelectForUser(string id)
        {
            return (await context.Penalties!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x!.Employee).ThenInclude(x => x!.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Member!.Id == id))!; // Finding the first penalty with the specified Member ID
        }

        // Checking if a penalty is already registered asynchronously
        public async Task<bool> IsRegistered(PenaltyPost tPost)
        {
            var penalties = await SelectAllFiltered(); // Getting all filtered penalties
            foreach (var penalty in penalties)
            {
                if (penalty.PenaltiedMembeId == tPost.PenaltiedMemberId &&
                    penalty.LoanId == tPost.LoanId &&
                    penalty.Active) // Checking if a penalty is registered with the same PenaltiedMember ID, Loan ID, and is active
                {
                    return true; // Penalty is already registered
                }
            }
            return false; // Penalty is not registered
        }

        // Adding a penalty to the context
        public void AddToContext(Penalty penalty)
        {
            context.Penalties!.Add(penalty); // Adding the penalty to the Penalties DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }

        // Banning a user asynchronously
        public async Task BanUser(string id)
        {
            var user = await userManager.FindByIdAsync(id); // Finding the user by ID
            user!.Banned = true; // Setting the user's banned status to true
            await userManager.UpdateAsync(user); // Updating the user's information
        }

        // Unbanning a user asynchronously
        public async Task UnBanUser(string id)
        {
            var user = await userManager.FindByIdAsync(id); // Finding the user by ID
            user!.Banned = false; // Setting the user's banned status to false
            await userManager.UpdateAsync(user); // Updating the user's information
        }
    }
}
