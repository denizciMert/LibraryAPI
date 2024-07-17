using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class AddressData : IQueryBase<Address>
    {
        private readonly ApplicationDbContext _context;

        public AddressData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> SelectAll()
        {
            return await _context.Addresses.Include(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.ApplicationUser).ToListAsync();
        }

        public async Task<Address> SelectForEntity(int id)
        {
            return await _context.Addresses.Include(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address> SelectForUser(string id)
        {
            return await _context.Addresses.Include(x => x.District).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.UserId == id);
        }

    }
}
