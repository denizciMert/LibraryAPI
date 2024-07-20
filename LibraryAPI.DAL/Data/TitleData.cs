using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class TitleData : IQueryBase<Title>
    {
        private readonly ApplicationDbContext _context;
        public TitleData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Title>> SelectAll()
        {
            return await _context.Titles.ToListAsync();
        }

        public async Task<Title> SelectForEntity(int id)
        {
            return await _context.Titles.FirstOrDefaultAsync(x=>x.Id==id);
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
            _context.Titles.Add(title);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
