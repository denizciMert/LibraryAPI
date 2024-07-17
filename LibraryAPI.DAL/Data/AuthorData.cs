using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class AuthorData : IQueryBase<Author>
    {
        private readonly ApplicationDbContext _context;

        public AuthorData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> SelectAll()
        {
            return await _context.Authors.Include(x => x.AuthorBooks).ThenInclude(x => x.Book).ToListAsync();
        }

        public async Task<Author> SelectForEntity(int id)
        {
            return await _context.Authors.Include(x => x.AuthorBooks).ThenInclude(x => x.Book).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Author> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
