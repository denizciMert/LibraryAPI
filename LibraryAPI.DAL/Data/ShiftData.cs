using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.ShiftDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class ShiftData(ApplicationDbContext context) : IQueryBase<Shift>
    {
        public async Task<List<Shift>> SelectAllFiltered()
        {
            return await context.Shifts.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Shift>> SelectAll()
        {
            return await context.Shifts.ToListAsync();
        }

        public async Task<Shift> SelectForEntity(int id)
        {
            return await context.Shifts.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Shift> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(ShiftPost tPost)
        {
            var shifts = await SelectAll();
            foreach (var shift in shifts)
            {
                if (shift.ShiftType == tPost.ShiftType)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Shift shift)
        {
            context.Shifts.Add(shift);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
