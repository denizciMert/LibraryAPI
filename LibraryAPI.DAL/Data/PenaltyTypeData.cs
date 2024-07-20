using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
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

        public async Task<bool> IsRegistered(PenaltyTypePost tPost)
        {
            var penaltyTypes = await SelectAll();
            foreach (var penaltyType in penaltyTypes)
            {
                if (penaltyType.PenaltyName == tPost.PenaltyType &&
                    penaltyType.AmountToPay == tPost.AmountToPay)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(PenaltyType penaltyType)
        {
            _context.PenaltyTypes.Add(penaltyType);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
