using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class PenaltyTypeData(ApplicationDbContext context) : IQueryBase<PenaltyType>
    {
        public async Task<List<PenaltyType>> SelectAllFiltered()
        {
            return await context.PenaltyTypes.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<PenaltyType>> SelectAll()
        {
            return await context.PenaltyTypes.ToListAsync();
        }

        public async Task<PenaltyType> SelectForEntity(int id)
        {
            return await context.PenaltyTypes.FirstOrDefaultAsync(x=>x.Id==id);
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
            context.PenaltyTypes.Add(penaltyType);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
