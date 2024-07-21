using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class TitleData(ApplicationDbContext context) : IQueryBase<Title>
    {
        public async Task<List<Title>> SelectAllFiltered()
        {
            return await context.Titles.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Title>> SelectAll()
        {
            return await context.Titles.ToListAsync();
        }

        public async Task<Title> SelectForEntity(int id)
        {
            return await context.Titles.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Title> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(TitlePost tPost)
        {
            var titles = await SelectAll();
            foreach (var title in titles)
            {
                if (title.TitleName == tPost.TitleName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Title title)
        {
            context.Titles.Add(title);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
