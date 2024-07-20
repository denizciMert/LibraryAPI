using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.LanguageDTO;
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

        public async Task<bool> IsRegistered(LanguagePost tPost)
        {
            var languages = await SelectAll();
            foreach (var language in languages)
            {
                if (language.LanguageCode == tPost.LanguageCode)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Language language)
        {
            _context.Languages.Add(language);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
