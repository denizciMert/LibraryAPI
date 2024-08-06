using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.CategoryDTO; // Importing the DTOs for Category
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Category-related data operations
    public class CategoryData(ApplicationDbContext context) : IQueryBase<Category>
    {
        // Selecting all categories with filters applied asynchronously
        public async Task<List<Category>> SelectAllFiltered()
        {
            return await context.Categories!
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted categories
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all categories asynchronously
        public async Task<List<Category>> SelectAll()
        {
            return await context.Categories!
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting a category by its ID asynchronously
        public async Task<Category> SelectForEntity(int id)
        {
            return (await context.Categories!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the category by ID
        }

        // Not implemented method for selecting a category by user ID
        public Task<Category> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Selecting a category by its name asynchronously
        public async Task<Category> SelectForEntityName(string name)
        {
            return (await context.Categories!
                .FirstOrDefaultAsync(x => x.CategoryName == name))!; // Finding the category by name
        }

        // Adding a category to the context
        public void AddToContext(Category category)
        {
            context.Categories!.Add(category); // Adding the category to the context
        }

        // Checking if a category is already registered asynchronously
        public async Task<bool> IsRegistered(CategoryPost tPost)
        {
            var categories = await SelectAllFiltered(); // Selecting all filtered categories
            foreach (var category in categories) // Iterating through each category
            {
                if (category.CategoryName == tPost.CategoryName) // Checking if the category name matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
