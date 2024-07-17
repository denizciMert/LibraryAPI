using LibraryAPI.DAL.Data.Interfaces;
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
    }
}
