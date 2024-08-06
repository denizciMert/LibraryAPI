using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.DepartmentDTO; // Importing the DTOs for Department
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Department-related data operations
    public class DepartmentData(ApplicationDbContext context) : IQueryBase<Department>
    {
        // Selecting all departments with filters applied asynchronously
        public async Task<List<Department>> SelectAllFiltered()
        {
            return await context.Departments!
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted departments
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all departments asynchronously
        public async Task<List<Department>> SelectAll()
        {
            return await context.Departments!
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting a department by its ID asynchronously
        public async Task<Department> SelectForEntity(int id)
        {
            return (await context.Departments!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the department by ID
        }

        // Not implemented method for selecting a department by user ID
        public Task<Department> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Checking if a department is already registered asynchronously
        public async Task<bool> IsRegistered(DepartmentPost tPost)
        {
            var departments = await SelectAllFiltered(); // Selecting all filtered departments
            foreach (var department in departments) // Iterating through each department
            {
                if (department.DepartmentName == tPost.DepartmentName) // Checking if the department name matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding a department to the context
        public void AddToContext(Department department)
        {
            context.Departments!.Add(department); // Adding the department to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
