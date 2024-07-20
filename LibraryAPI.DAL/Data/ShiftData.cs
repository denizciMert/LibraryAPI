using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.ShiftDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class ShiftData : IQueryBase<Shift>
    {
        private readonly ApplicationDbContext _context;

        public ShiftData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> SelectAll()
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task<Shift> SelectForEntity(int id)
        {
            return await _context.Shifts.FirstOrDefaultAsync(x=>x.Id==id);
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
            _context.Shifts.Add(shift);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
