using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class CityData : IQueryBase<City>
    {
        private readonly ApplicationDbContext _context;
        public CityData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> SelectAll()
        {
            return await _context.Cities.Include(x=>x.Country).ToListAsync();
        }

        public async Task<City> SelectForEntity(int id)
        {
            return await _context.Cities.Include(x => x.Country).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<City> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
