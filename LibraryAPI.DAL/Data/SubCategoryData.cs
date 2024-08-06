using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.SubCategoryDTO; // Importing the DTOs for SubCategory
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for SubCategory-related data operations
    public class SubCategoryData(ApplicationDbContext context) : IQueryBase<SubCategory>
    {
        // Selecting all subcategories with filters applied asynchronously
        public async Task<List<SubCategory>> SelectAllFiltered()
        {
            return await context.SubCategories!
                .Include(x => x.Category) // Including the related Category
                .Where(x => x.State != State.Silindi) // Filtering out subcategories with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all subcategories asynchronously
        public async Task<List<SubCategory>> SelectAll()
        {
            return await context.SubCategories!
                .Include(x => x.Category) // Including the related Category
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a subcategory by ID asynchronously
        public async Task<SubCategory> SelectForEntity(int id)
        {
            return (await context.SubCategories!
                .Include(x => x.Category) // Including the related Category
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first subcategory with the specified ID
        }

        // Not implemented for this entity
        public Task<SubCategory> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a subcategory is already registered asynchronously
        public async Task<bool> IsRegistered(SubCategoryPost tPost)
        {
            var subCategories = await SelectAllFiltered(); // Getting all filtered subcategories
            foreach (var subCategory in subCategories)
            {
                if (subCategory.CategoryId == tPost.CategoryId &&
                    subCategory.SubCategoryName == tPost.SubCategoryName) // Checking if a subcategory is registered with the same Category ID and name
                {
                    return true; // SubCategory is already registered
                }
            }
            return false; // SubCategory is not registered
        }

        // Adding a subcategory to the context
        public void AddToContext(SubCategory subCategory)
        {
            context.SubCategories!.Add(subCategory); // Adding the subcategory to the SubCategories DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
