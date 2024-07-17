using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class ReservationData : IQueryBase<Reservation>
    {
        private readonly ApplicationDbContext _context;

        public ReservationData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> SelectAll()
        {
            return await _context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .ToListAsync();
        }

        public async Task<Reservation> SelectForEntity(int id)
        {
            return await _context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Reservation> SelectForUser(string id)
        {
            return await _context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x => x.Member.Id == id);
        }
    }
}
