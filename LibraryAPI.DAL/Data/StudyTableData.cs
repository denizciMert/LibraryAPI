using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.StudyTableDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class StudyTableData : IQueryBase<StudyTable>
    {
        private readonly ApplicationDbContext _context;

        public StudyTableData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudyTable>> SelectAll()
        {
            return await _context.StudyTables.ToListAsync();
        }

        public async Task<StudyTable> SelectForEntity(int id)
        {
            return await _context.StudyTables.FirstOrDefaultAsync(x=>x.Id==id);
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
            _context.StudyTables.Add(studyTable);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
