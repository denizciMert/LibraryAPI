using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for query operations
using LibraryAPI.Entities.DTOs.AuthorDTO; // Importing the DTOs for Author
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Author-related data operations
    public class AuthorData(ApplicationDbContext context) : IQueryBase<Author>
    {
        // Selecting all authors with filters applied asynchronously
        public async Task<List<Author>> SelectAllFiltered()
        {
            return await context.Authors!
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted authors
                .Include(x => x.AuthorBooks)! // Including related AuthorBooks
                .ThenInclude(x => x.Book) // Including related Books
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all authors asynchronously
        public async Task<List<Author>> SelectAll()
        {
            return await context.Authors!
                .Include(x => x.AuthorBooks)! // Including related AuthorBooks
                .ThenInclude(x => x.Book) // Including related Books
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting an author by its ID asynchronously
        public async Task<Author> SelectForEntity(int id)
        {
            return (await context.Authors!
                .Include(x => x.AuthorBooks)! // Including related AuthorBooks
                .ThenInclude(x => x.Book) // Including related Books
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the author by ID
        }

        // Not implemented method for selecting an author by user ID
        public Task<Author> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Selecting books for an author by author ID asynchronously
        public async Task<List<string>> SelectBooks(int authorId)
        {
            return await context.AuthorBooks!
                .Include(x => x.Author) // Including related Author
                .Include(x => x.Book) // Including related Books
                .Where(x => x.AuthorsId == authorId) // Filtering by author ID
                .Select(x => x.Book!.BookTitle) // Selecting the book titles
                .ToListAsync(); // Converting the result to a list
        }

        // Checking if an author is already registered asynchronously
        public async Task<bool> IsRegistered(AuthorPost tPost)
        {
            var authors = await SelectAllFiltered(); // Selecting all filtered authors
            foreach (var author in authors) // Iterating through each author
            {
                if (author.AuthorFullName == tPost.AuthorName) // Checking if the author name matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding an author to the context
        public void AddToContext(Author author)
        {
            context.Authors!.Add(author); // Adding the author to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
