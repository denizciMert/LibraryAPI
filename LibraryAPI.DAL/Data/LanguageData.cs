using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class LanguageData : IQueryBase<Language>
    {
        private readonly ApplicationDbContext _context;

        public LanguageData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> SelectAll()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Language> SelectForEntity(int id)
        {
            return await _context.Languages.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Language> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
