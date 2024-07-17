using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class LocationData : IQueryBase<Location>
    {
        private readonly ApplicationDbContext _context;

        public LocationData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> SelectAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> SelectForEntity(int id)
        {
            return await _context.Locations.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Location> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
