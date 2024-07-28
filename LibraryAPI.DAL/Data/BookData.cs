using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.BookDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class BookData(ApplicationDbContext context) : IQueryBase<Book>
    {
        public async Task<List<Book>> SelectAllFiltered()
        {
            return await context.Books
                .Include(x => x.AuthorBooks)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookLanguages)
                .ThenInclude(x => x.Language)
                .Include(x => x.BookSubCategories)
                .ThenInclude(x => x.SubCategory)
                .Include(x => x.Publisher)
                .Include(x => x.Location)
                .Include(x=>x.BookCopies)
                .Where(x=>x.Banned==false)
                .Where(x=>x.State!=State.Silindi)
                .ToListAsync();
        }

        public async Task<List<Book>> SelectAll()
        {
            return await context.Books
                .Include(x => x.AuthorBooks)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookLanguages)
                .ThenInclude(x => x.Language)
                .Include(x=>x.BookSubCategories)
                .ThenInclude(x=>x.SubCategory)
                .Include(x=>x.Publisher)
                .Include(x => x.Location)
                .ToListAsync();
        }

        public async Task<Book> SelectForEntity(int id)
        {
            return await context.Books
                .Include(x => x.AuthorBooks)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookLanguages)
                .ThenInclude(x => x.Language)
                .Include(x => x.BookSubCategories)
                .ThenInclude(x => x.SubCategory)
                .Include(x => x.Publisher)
                .Include(x => x.Location)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Book> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(BookPost tPost)
        {
            var books = await SelectAllFiltered();
            foreach (var book in books)
            {
                if (book.Isbn==tPost.Isbn)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Book book)
        {
            context.Books.Add(book);
        }

        public void AddToCopyContext(BookCopy copy)
        {
            context.BookCopies.Add(copy);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
