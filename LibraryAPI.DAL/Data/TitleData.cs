using LibraryAPI.DAL.Data.Interfaces;
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
    }
}
