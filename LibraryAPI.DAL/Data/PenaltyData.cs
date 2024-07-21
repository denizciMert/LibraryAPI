using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.PenaltyDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class PenaltyData(ApplicationDbContext context) : IQueryBase<Penalty>
    {
        public async Task<List<Penalty>> SelectAllFiltered()
        {
            return await context.Penalties
                .Include(x => x.Member)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x.Employee)
                .ThenInclude(x => x.ApplicationUser)
                .Where(x=>x.Active==true)
                .Where(x=>x.State!=State.Silindi)
                .ToListAsync();
        }

        public async Task<List<Penalty>> SelectAll()
        {
            return await context.Penalties
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x=>x.Loan).ThenInclude(x=>x.Employee).ThenInclude(x=>x.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Penalty> SelectForEntity(int id)
        {
            return await context.Penalties
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Penalty> SelectForUser(string id)
        {
            return await context.Penalties
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x=>x.Member.Id==id);
        }

        public async Task<bool> IsRegistered(PenaltyPost tPost)
        {
            var penalties = await SelectAll();
            foreach (var penalty in penalties)
            {
                if (penalty.PenaltiedMembeId == tPost.PenaltiedMemberId &&
                    penalty.LoanId == tPost.LoanId &&
                    penalty.Active == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Penalty penalty)
        {
            context.Penalties.Add(penalty);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
