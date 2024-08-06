using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.ReservationDTO; // Importing the DTOs for Reservation
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Reservation-related data operations
    public class ReservationData(ApplicationDbContext context) : IQueryBase<Reservation>
    {
        // Selecting all reservations with filters applied asynchronously
        public async Task<List<Reservation>> SelectAllFiltered()
        {
            return await context.Reservations!
                .Include(x => x.Member)
                .ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.Employee)
                .ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.StudyTable)
                .Where(x => x.State != State.Silindi) // Filtering out reservations with state "Silindi"
                .Where(x => x.Active == true) // Filtering only active reservations
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all reservations asynchronously
        public async Task<List<Reservation>> SelectAll()
        {
            return await context.Reservations!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.StudyTable)
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a reservation by ID asynchronously
        public async Task<Reservation> SelectForEntity(int id)
        {
            return (await context.Reservations!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first reservation with the specified ID
        }

        // Selecting a reservation by user ID asynchronously
        public async Task<Reservation> SelectForUser(string id)
        {
            return (await context.Reservations!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.StudyTable)
                .FirstOrDefaultAsync(x => x.Member!.Id == id))!; // Finding the first reservation with the specified Member ID
        }

        // Checking if a reservation is already registered asynchronously
        public async Task<bool> IsRegistered(ReservationPost tPost)
        {
            var reservations = await SelectAllFiltered(); // Getting all filtered reservations
            foreach (var reservation in reservations)
            {
                if (reservation.MemberId == tPost.MemberId ||
                    reservation.TableId == tPost.TableId) // Checking if a reservation is registered with the same Member ID or Table ID
                {
                    return true; // Reservation is already registered
                }
            }
            return false; // Reservation is not registered
        }

        // Adding a reservation to the context
        public void AddToContext(Reservation reservation)
        {
            context.Reservations!.Add(reservation); // Adding the reservation to the Reservations DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
