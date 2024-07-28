using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class AuthorData(ApplicationDbContext context) : IQueryBase<Author>
    {
        public async Task<List<Author>> SelectAllFiltered()
        {
            return await context.Authors.Where(x=>x.State!=State.Silindi).Include(x => x.AuthorBooks).ThenInclude(x => x.Book).ToListAsync();
        }

        public async Task<List<Author>> SelectAll()
        {
            return await context.Authors.Include(x => x.AuthorBooks).ThenInclude(x => x.Book).ToListAsync();
        }

        public async Task<Author> SelectForEntity(int id)
        {
            return await context.Authors.Include(x => x.AuthorBooks).ThenInclude(x => x.Book).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Author> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> SelectBooks(int authorId)
        {
            return await context.AuthorBooks
                .Include(x => x.Author)
                .Include(x=>x.Book)
                .Where(x => x.AuthorsId == authorId)
                .Select(x=>x.Book.BookTitle)
                .ToListAsync();
        }

        public async Task<bool> IsRegistered(AuthorPost tPost)
        {
            var authors = await SelectAllFiltered();
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
            context.Authors.Add(author);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
