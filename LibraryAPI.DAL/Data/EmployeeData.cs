using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.EmployeeDTO; // Importing the DTOs for Employee
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.AspNetCore.Identity; // Importing Identity framework functionalities
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Employee-related data operations
    public class EmployeeData(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IQueryBase<Employee>
    {
        // Selecting all employees with filters applied asynchronously
        public async Task<List<Employee>> SelectAllFiltered()
        {
            return await context.Employees!
                .Include(x => x.ApplicationUser).ThenInclude(x => x!.Country) // Including related ApplicationUser and Country entities
                .Include(x => x.Department) // Including related Department entity
                .Include(x => x.Title) // Including related Title entity
                .Include(x => x.Shift) // Including related Shift entity
                .Where(x => x.ApplicationUser!.State != State.Silindi) // Filtering by non-deleted employees
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all employees asynchronously
        public async Task<List<Employee>> SelectAll()
        {
            return await context.Employees!
                .Include(x => x.ApplicationUser).ThenInclude(x => x!.Country) // Including related ApplicationUser and Country entities
                .Include(x => x.Department) // Including related Department entity
                .Include(x => x.Title) // Including related Title entity
                .ToListAsync(); // Converting the result to a list
        }

        // Method not implemented for selecting an employee by ID
        public Task<Employee> SelectForEntity(int id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Adding a role to a user asynchronously
        public async Task AddRoleToUser(ApplicationUser user, string role)
        {
            await userManager.AddToRoleAsync(user, role); // Adding the role to the user
        }

        // Selecting an employee by user ID asynchronously
        public async Task<Employee> SelectForUser(string id)
        {
            var employee = await context.Employees!
                .Include(x => x.ApplicationUser).ThenInclude(x => x!.Country) // Including related ApplicationUser and Country entities
                .Include(x => x.Department) // Including related Department entity
                .Include(x => x.Shift) // Including related Shift entity
                .Include(x => x.Title) // Including related Title entity
                .FirstOrDefaultAsync(x => x.Id == id); // Finding the employee by ID

            if (employee == null)
            {
                return null!; // Returning null if the employee is not found
            }

            return employee; // Returning the found employee
        }

        // Checking if an employee is already registered asynchronously
        public async Task<bool> IsRegistered(EmployeePost tPost)
        {
            var employees = await SelectAllFiltered(); // Selecting all filtered employees
            foreach (var employee in employees) // Iterating through each employee
            {
                if (employee.ApplicationUser!.IdentityNo == tPost.IdentityNo ||
                    employee.ApplicationUser.UserName == tPost.UserName ||
                    employee.ApplicationUser.Email == tPost.Email) // Checking if identity number, username, or email matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Getting user addresses by user ID asynchronously
        public async Task<List<string>> GetUserAddresses(string userId)
        {
            var addresses = await context.Addresses!
                .Where(x => x.UserId == userId && x.State != State.Silindi) // Filtering by user ID and non-deleted addresses
                .Select(a => $"Adres: {a.AddressString} {a.District!.DistrictName} {a.District!.City!.CityName} {a.District!.City!.Country!.CountryName}") // Formatting the address string
                .ToListAsync(); // Converting the result to a list

            return addresses; // Returning the list of addresses
        }

        // Saving a new user asynchronously
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
            await userManager.UpdateAsync(user); // Updating the user
        }

        // Deleting a user asynchronously
        public async Task DeleteUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user); // Marking the user as deleted
        }

        // Adding an employee to the context
        public void AddToContext(Employee employee)
        {
            context.Employees!.Add(employee); // Adding the employee to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
