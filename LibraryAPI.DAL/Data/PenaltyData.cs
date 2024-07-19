using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class PenaltyData : IQueryBase<Penalty>
    {
        private readonly ApplicationDbContext _context;

        public PenaltyData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Penalty>> SelectAll()
        {
            return await _context.Penalties
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x=>x.Loan).ThenInclude(x=>x.Employee).ThenInclude(x=>x.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Penalty> SelectForEntity(int id)
        {
            return await _context.Penalties
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Penalty> SelectForUser(string id)
        {
            return await _context.Penalties
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.PenaltyType)
                .Include(x => x.Loan).ThenInclude(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x=>x.Member.Id==id);
        }
    }
}
