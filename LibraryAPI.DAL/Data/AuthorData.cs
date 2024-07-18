using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AuthorDTO;
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

        public async Task<List<string>> SelectBooks(int authorId)
        {
            return await _context.AuthorBooks
                .Include(x => x.Author)
                .Include(x=>x.Book)
                .Where(x => x.AuthorsId == authorId)
                .Select(x=>x.Book.BookTitle)
                .ToListAsync();
        }

        public async Task<bool> IsRegistered(AuthorPost tPost)
        {
            var authors = await SelectAll();
            foreach (var author in authors)
            {
                if (author.AuthorFullName==tPost.AuthorName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Author author)
        {
            _context.Authors.Add(author);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
