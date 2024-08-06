using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.LanguageDTO; // Importing the DTOs for Language
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Language-related data operations
    public class LanguageData(ApplicationDbContext context) : IQueryBase<Language>
    {
        // Selecting all languages with filters applied asynchronously
        public async Task<List<Language>> SelectAllFiltered()
        {
            return await context.Languages!
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted languages
                .ToListAsync(); // Converting the result to a list
        }

        // Selecting all languages asynchronously
        public async Task<List<Language>> SelectAll()
        {
            return await context.Languages!.ToListAsync(); // Converting the result to a list
        }

        // Selecting a language by ID asynchronously
        public async Task<Language> SelectForEntity(int id)
        {
            return (await context.Languages!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the language by ID
        }

        // Method not implemented for selecting a language by user ID
        public Task<Language> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as it's not implemented
        }

        // Checking if a language is already registered asynchronously
        public async Task<bool> IsRegistered(LanguagePost tPost)
        {
            var languages = await SelectAllFiltered(); // Selecting all filtered languages
            foreach (var language in languages) // Iterating through each language
            {
                if (language.LanguageCode == tPost.LanguageCode) // Checking if language code matches
                {
                    return true; // Returning true if a match is found
                }
            }
            return false; // Returning false if no match is found
        }

        // Adding a language to the context
        public void AddToContext(Language language)
        {
            context.Languages!.Add(language); // Adding the language to the context
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the database
        }
    }
}
