using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.LocationDTO;
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

        public async Task<bool> IsRegistered(LocationPost tPost)
        {
            var locations = await SelectAll();
            foreach (var location in locations)
            {
                if (location.ShelfCode == tPost.ShelfCode)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Location location)
        {
            _context.Locations.Add(location);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
