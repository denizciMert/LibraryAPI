using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.MemberDTO; // Importing the DTOs for Member
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.AspNetCore.Identity; // Importing the ASP.NET Core Identity functionalities
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Member-related data operations
    public class MemberData(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IQueryBase<Member>
    {
        // Selecting all members with filters applied asynchronously
        public async Task<List<Member>> SelectAllFiltered()
        {
            return await context.Members!
                .Include(x => x.ApplicationUser)
                .ThenInclude(x => x!.Country)
                .Include(x => x.ApplicationUser!.Addresses)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .Where(x => x.ApplicationUser!.State != State.Silindi) // Filtering out members with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all members asynchronously
        public async Task<List<Member>> SelectAll()
        {
            return await context.Members!
                .Include(x => x.ApplicationUser)
                .ThenInclude(x => x!.Country)
                .Include(x => x.ApplicationUser!.Addresses)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Not implemented for this entity
        public Task<Member> SelectForEntity(int id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Selecting a member by user ID asynchronously
        public async Task<Member> SelectForUser(string id)
        {
            var member = await context.Members!
                .Include(x => x.ApplicationUser).ThenInclude(x => x!.Country)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .FirstOrDefaultAsync(x => x.Id == id); // Finding the first member with the specified ID
            if (member == null)
            {
                return null!; // Returning null if the member is not found
            }

            return member; // Returning the found member
        }

        // Checking if a member is already registered asynchronously
        public async Task<bool> IsRegistered(MemberPost tPost)
        {
            var members = await SelectAllFiltered(); // Getting all filtered members
            foreach (var member in members)
            {
                if (member.ApplicationUser!.Email == tPost.Email ||
                    member.ApplicationUser.IdentityNo == tPost.IdentityNo ||
                    member.ApplicationUser.UserName == tPost.UserName) // Checking if a member is registered with the same email, identity number, or username
                {
                    return true; // Member is already registered
                }
            }
            return false; // Member is not registered
        }

        // Getting the addresses of a user by user ID asynchronously
        public async Task<List<string>> GetUserAddresses(string userId)
        {
            var addresses = await context.Addresses!
                .Where(x => x.UserId == userId && x.State != State.Silindi) // Filtering out addresses with state "Silindi"
                .Select(a => $"Adres: {a.AddressString} {a.District!.DistrictName} {a.District!.City!.CityName} {a.District!.City!.Country!.CountryName}") // Formatting the address information
                .ToListAsync(); // Executing the query and returning the result as a list

            return addresses; // Returning the list of addresses
        }

        // Getting the loans of a user by user ID asynchronously
        public async Task<List<string>> GetUserLoans(string userId)
        {
            var loans = await context.Loans!
                .Where(x => x.LoanedMemberId == userId && x.Active == true && x.State != State.Silindi) // Filtering out loans with state "Silindi" and inactive loans
                .Include(x => x.Book)
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser)
                .Select(a => $"Kitap: {a.Book!.BookTitle}, Kopya: {a.CopyNo}, Çalışan: {a.Employee!.ApplicationUser!.UserName}") // Formatting the loan information
                .ToListAsync(); // Executing the query and returning the result as a list

            return loans; // Returning the list of loans
        }

        // Getting the penalties of a user by user ID asynchronously
        public async Task<List<string>> GetUserPenalties(string userId)
        {
            var penalties = await context.Penalties!
                .Where(x => x.PenaltiedMembeId == userId && x.Active == true && x.State != State.Silindi) // Filtering out penalties with state "Silindi" and inactive penalties
                .Include(x => x.Loan)
                .Include(x => x.PenaltyType)
                .Select(a => $"Ceza Tipi: {a.PenaltyType!.PenaltyName}, Ceza Tutarı: {a.PenaltyType.AmountToPay}") // Formatting the penalty information
                .ToListAsync(); // Executing the query and returning the result as a list

            return penalties; // Returning the list of penalties
        }

        // Adding a role to a user asynchronously
        public async Task AddRoleToUser(ApplicationUser user, string role)
        {
            await userManager.AddToRoleAsync(user, role); // Adding the specified role to the user
        }

        // Saving a user with a password asynchronously
        public async Task SaveUser(ApplicationUser user, string password)
        {
            await userManager.CreateAsync(user, password); // Creating the user with the specified password
        }

        // Updating a user's password asynchronously
        public async Task UpdateUserPassword(ApplicationUser user, string currentPassword, string newPassword)
        {
            await userManager.ChangePasswordAsync(user, currentPassword, newPassword); // Changing the user's password
        }

        // Updating a user asynchronously
        public async Task UpdateUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user); // Updating the user's information
        }

        // Deleting a user asynchronously
        public async Task DeleteUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user); // Deleting (updating the state of) the user
        }

        // Adding a member to the context
        public void AddToContext(Member member)
        {
            context.Members!.Add(member); // Adding the member to the Members DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
