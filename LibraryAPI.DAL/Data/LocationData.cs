using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.LocationDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class LocationData(ApplicationDbContext context) : IQueryBase<Location>
    {
        public async Task<List<Location>> SelectAllFiltered()
        {
            return await context.Locations.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Location>> SelectAll()
        {
            return await context.Locations.ToListAsync();
        }

        public async Task<Location> SelectForEntity(int id)
        {
            return await context.Locations.FirstOrDefaultAsync(x=>x.Id==id);
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
            context.Locations.Add(location);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
