using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.StudyTableDTO; // Importing the DTOs for StudyTable
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for StudyTable-related data operations
    public class StudyTableData(ApplicationDbContext context) : IQueryBase<StudyTable>
    {
        // Selecting all study tables with filters applied asynchronously
        public async Task<List<StudyTable>> SelectAllFiltered()
        {
            return await context.StudyTables!
                .Where(x => x.State != State.Silindi) // Filtering out study tables with state "Silindi"
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all study tables asynchronously
        public async Task<List<StudyTable>> SelectAll()
        {
            return await context.StudyTables!.ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a study table by ID asynchronously
        public async Task<StudyTable> SelectForEntity(int id)
        {
            return (await context.StudyTables!
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first study table with the specified ID
        }

        // Not implemented for this entity
        public Task<StudyTable> SelectForUser(string id)
        {
            throw new NotImplementedException(); // Throwing an exception as this method is not implemented
        }

        // Checking if a study table is already registered asynchronously
        public async Task<bool> IsRegistered(StudyTablePost tPost)
        {
            var studyTables = await SelectAllFiltered(); // Getting all filtered study tables
            foreach (var studyTable in studyTables)
            {
                if (studyTable.TableCode == tPost.TableCode) // Checking if a study table is registered with the same table code
                {
                    return true; // Study table is already registered
                }
            }
            return false; // Study table is not registered
        }

        // Adding a study table to the context
        public void AddToContext(StudyTable studyTable)
        {
            context.StudyTables!.Add(studyTable); // Adding the study table to the StudyTables DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
