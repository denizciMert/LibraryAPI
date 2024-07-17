using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class DistrictData : IQueryBase<District>
    {
        private readonly ApplicationDbContext _context;
        public DistrictData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<District>> SelectAll()
        {
            return await _context.Districts.Include(x => x.City).ThenInclude(x => x.Country).ToListAsync();
        }

        public async Task<District> SelectForEntity(int id)
        {
            return await _context.Districts.Include(x => x.City).ThenInclude(x => x.Country).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<District> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
