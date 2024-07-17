using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class MemberData : IQueryBase<Member>
    {
        private readonly ApplicationDbContext _context;

        public MemberData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> SelectAll()
        {
            return await _context.Members
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .ToListAsync();
        }

        public async Task<Member> SelectForEntity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> SelectForUser(string id)
        {
            return await _context.Members
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
