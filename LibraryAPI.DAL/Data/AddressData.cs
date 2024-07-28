using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class AddressData(ApplicationDbContext context) : IQueryBase<Address>
    {
        public async Task<List<Address>> SelectAllFiltered()
        {
            return await context.Addresses
                    .Include(x => x.District)
                    .ThenInclude(x => x.City)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.ApplicationUser)
                    .Where(x => x.State != State.Silindi)
                    .Where(x => x.ApplicationUser.Banned != true)
                    .Where(x => x.ApplicationUser.State != State.Silindi)
                    .Where(x => x.District.State != State.Silindi)
                    .Where(x => x.District.City.State != State.Silindi)
                    .Where(x => x.District.City.Country.State != State.Silindi)
                    .ToListAsync();
        }

        public async Task<List<Address>> SelectAll()
        {
            return await context.Addresses
                .Include(x => x.District)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Address> SelectForEntity(int id)
        {
            return await context.Addresses
                .Include(x => x.District)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address> SelectForUser(string id)
        {
            return await context.Addresses
                .Include(x => x.District)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<bool> IsRegistered(AddressPost tPost)
        {
            var addresses = await SelectAllFiltered();
            foreach (var address in  addresses)
            {
                if (address.UserId==tPost.UserId &&
                    address.DistrictId==tPost.DistrictId &&
                    address.AddressString==tPost.AddressString)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Address address)
        {
            context.Addresses.Add(address);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
