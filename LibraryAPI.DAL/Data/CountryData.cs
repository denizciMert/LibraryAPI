using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.CountryDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class CountryData(ApplicationDbContext context) : IQueryBase<Country>
    {
        public async Task<List<Country>> SelectAllFiltered()
        {
            return await context.Countries.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Country>> SelectAll()
        {
            return await context.Countries.ToListAsync();
        }

        public async Task<Country> SelectForEntity(int id)
        {
            return await context.Countries.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Country> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(CountryPost tPost)
        {
            var countries = await SelectAll();
            foreach (var country in countries)
            {
                if (country.CountryName == tPost.CountryName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Country country)
        {
            context.Countries.Add(country);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
