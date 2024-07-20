using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.SubCategoryDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class SubCategoryData : IQueryBase<SubCategory>
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubCategory>> SelectAll()
        {
            return await _context.SubCategories.Include(x=>x.Category).ToListAsync();
        }

        public async Task<SubCategory> SelectForEntity(int id)
        {
            return await _context.SubCategories.Include(x => x.Category).FirstOrDefaultAsync(x=>x.Id ==id);
        }

        public async Task<SubCategory> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(SubCategoryPost tPost)
        {
            var subCategories = await SelectAll();
            foreach (var subCategory in subCategories)
            {
                if (subCategory.CategoryId == tPost.CategoryId &&
                    subCategory.SubCategoryName == tPost.SubCategoryName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
