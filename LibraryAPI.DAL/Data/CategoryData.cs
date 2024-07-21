using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class CategoryData(ApplicationDbContext context) : IQueryBase<Category>
    {
        public async Task<List<Category>> SelectAllFiltered()
        {
            return await context.Categories
                .Where(x=>x.State!=State.Silindi)
                .ToListAsync();
        }

        public async Task<List<Category>> SelectAll()
        {
            return await context.Categories
                .ToListAsync();
        }

        public async Task<Category> SelectForEntity(int id)
        {
            return await context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> SelectForEntityName(string name)
        {
            return await context.Categories
                .FirstOrDefaultAsync(x => x.CategoryName == name);
        }

        public void AddToContext(Category category)
        {
            context.Categories.Add(category);
        }

        public async Task<bool> IsRegistered(CategoryPost tPost)
        {
            var categories = await SelectAll();
            foreach (var category in categories)
            {
                if (category.CategoryName == tPost.CategoryName)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
