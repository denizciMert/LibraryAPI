using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.StudyTableDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class StudyTableData(ApplicationDbContext context) : IQueryBase<StudyTable>
    {
        public async Task<List<StudyTable>> SelectAllFiltered()
        {
            return await context.StudyTables.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<StudyTable>> SelectAll()
        {
            return await context.StudyTables.ToListAsync();
        }

        public async Task<StudyTable> SelectForEntity(int id)
        {
            return await context.StudyTables.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<StudyTable> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(StudyTablePost tPost)
        {
            var studyTables = await SelectAll();
            foreach (var studyTable in studyTables)
            {
                if (studyTable.TableCode == tPost.TableCode)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(StudyTable studyTable)
        {
            context.StudyTables.Add(studyTable);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
