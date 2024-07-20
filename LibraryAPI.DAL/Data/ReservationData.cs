using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.ReservationDTO;
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
                .Include(x=>x.Employee).ThenInclude(x=>x.ApplicationUser)
                .Include(x => x.StudyTable)
                .ToListAsync();
        }

        public async Task<Reservation> SelectForEntity(int id)
        {
            return await _context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Reservation> SelectForUser(string id)
        {
            return await _context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x => x.Member.Id == id);
        }

        public async Task<bool> IsRegistered(ReservationPost tPost)
        {
            var reservations = await SelectAll();
            foreach (var reservation in reservations)
            {
                if (reservation.MemberId == tPost.MemberId &&
                    reservation.TableId == tPost.TableId &&
                    reservation.ReservationEnd > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
