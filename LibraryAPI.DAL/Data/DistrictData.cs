using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.DistrictDTO;
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

        public async Task<bool> IsRegistered(DistrictPost tPost)
        {
            var districts = await SelectAll();
            foreach (var district in districts)
            {
                if (district.CityId == tPost.CityId &&
                    district.DistrictName == tPost.District)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(District district)
        {
            _context.Districts.Add(district);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
