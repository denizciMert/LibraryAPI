using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.ReservationDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class ReservationData(ApplicationDbContext context) : IQueryBase<Reservation>
    {
        public async Task<List<Reservation>> SelectAllFiltered()
        {
            return await context.Reservations
                .Include(x => x.Member)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .Where(x=>x.State!=State.Silindi)
                .Where(x=>x.Active==true)
                .ToListAsync();
        }

        public async Task<List<Reservation>> SelectAll()
        {
            return await context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x=>x.Employee).ThenInclude(x=>x.ApplicationUser)
                .Include(x => x.StudyTable)
                .ToListAsync();
        }

        public async Task<Reservation> SelectForEntity(int id)
        {
            return await context.Reservations
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Reservation> SelectForUser(string id)
        {
            return await context.Reservations
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
            context.Reservations.Add(reservation);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
