using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.CountryDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class CountryData : IQueryBase<Country>
    {
        private readonly ApplicationDbContext _context;

        public CountryData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> SelectAll()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> SelectForEntity(int id)
        {
            return await _context.Countries.FirstOrDefaultAsync(x=>x.Id==id);
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
            _context.Countries.Add(country);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
