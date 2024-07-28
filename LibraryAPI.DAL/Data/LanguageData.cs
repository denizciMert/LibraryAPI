using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.LanguageDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class LanguageData(ApplicationDbContext context) : IQueryBase<Language>
    {
        public async Task<List<Language>> SelectAllFiltered()
        {
            return await context.Languages.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Language>> SelectAll()
        {
            return await context.Languages.ToListAsync();
        }

        public async Task<Language> SelectForEntity(int id)
        {
            return await context.Languages.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Language> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(LanguagePost tPost)
        {
            var languages = await SelectAllFiltered();
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
            context.Languages.Add(language);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
