using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class PenaltyTypeData : IQueryBase<PenaltyType>
    {
        private readonly ApplicationDbContext _context;

        public PenaltyTypeData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PenaltyType>> SelectAll()
        {
            return await _context.PenaltyTypes.ToListAsync();
        }

        public async Task<PenaltyType> SelectForEntity(int id)
        {
            return await _context.PenaltyTypes.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<PenaltyType> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
