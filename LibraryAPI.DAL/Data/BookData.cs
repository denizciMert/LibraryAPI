using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.BookDTO; // Importing the DTOs for Book
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Book-related data operations
    public class BookData(ApplicationDbContext context) : IQueryBase<Book>
    {
        // Selecting all books with filters applied asynchronously
        public async Task<List<Book>> SelectAllFiltered()
        {
            return await context.Books!
                .Include(x => x.AuthorBooks)! // Including related AuthorBooks
                .ThenInclude(x => x.Author) // Including related Authors
                .Include(x => x.BookLanguages)! // Including related BookLanguages
                .ThenInclude(x => x.Language) // Including related Languages
                .Include(x => x.BookSubCategories)! // Including related BookSubCategories
                .ThenInclude(x => x.SubCategory) // Including related SubCategories
                .Include(x => x.Publisher) // Including related Publishers
                .Include(x => x.Location) // Including related Locations
                .Include(x => x.BookCopies) // Including related BookCopies
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted books
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all books asynchronously
        public async Task<List<Book>> SelectAll()
        {
            return await context.Books!
                .Include(x => x.AuthorBooks)! // Including related AuthorBooks
                .ThenInclude(x => x.Author) // Including related Authors
                .Include(x => x.BookLanguages)! // Including related BookLanguages
                .ThenInclude(x => x.Language) // Including related Languages
                .Include(x => x.BookSubCategories)! // Including related BookSubCategories
                .ThenInclude(x => x.SubCategory) // Including related SubCategories
                .Include(x => x.Publisher) // Including related Publishers
                .Include(x => x.Location) // Including related Locations
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting a book by its ID asynchronously
        public async Task<Book> SelectForEntity(int id)
        {
            return (await context.Books!
                .Include(x => x.AuthorBooks)! // Including related AuthorBooks
                .ThenInclude(x => x.Author) // Including related Authors
                .Include(x => x.BookLanguages)! // Including related BookLanguages
                .ThenInclude(x => x.Language) // Including related Languages
                .Include(x => x.BookSubCategories)! // Including related BookSubCategories
                .ThenInclude(x => x.SubCategory) // Including related SubCategories
                .Include(x => x.Publisher) // Including related Publishers
                .Include(x => x.Location) // Including related Locations
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the book by ID
        }

        // Not implemented method for selecting a book by user ID
        public Task<Book> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Checking if a book is already registered asynchronously
        public async Task<bool> IsRegistered(BookPost tPost)
        {
            var books = await SelectAllFiltered(); // Selecting all filtered books
            foreach (var book in books) // Iterating through each book
            {
                if (book.Isbn == tPost.Isbn) // Checking if the ISBN matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding a book to the context
        public void AddToContext(Book book)
        {
            context.Books!.Add(book); // Adding the book to the context
        }

        // Adding a book copy to the context
        public void AddToCopyContext(BookCopy copy)
        {
            context.BookCopies!.Add(copy); // Adding the book copy to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
