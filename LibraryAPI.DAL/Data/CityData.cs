using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class CityData(ApplicationDbContext context) : IQueryBase<City>
    {
        public async Task<List<City>> SelectAllFiltered()
        {
            return await context.Cities
                .Include(x => x.Country)
                .Where(x=>x.State!=State.Silindi)
                .ToListAsync();
        }

        public async Task<List<City>> SelectAll()
        {
            return await context.Cities
                .Include(x=>x.Country)
                .ToListAsync();
        }

        public async Task<City> SelectForEntity(int id)
        {
            return await context.Cities
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<City> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(CityPost tPost)
        {
            var cities = await SelectAll();
            foreach (var city in cities)
            {
                if (city.CityName == tPost.CityName &&
                    city.CountryId == tPost.CountryId)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(City city)
        {
            context.Cities.Add(city);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
