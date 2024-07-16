using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.DAL;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyTablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudyTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StudyTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudyTable>>> GetStudyTables()
        {
            return await _context.StudyTables.ToListAsync();
        }

        // GET: api/StudyTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudyTable>> GetStudyTable(int id)
        {
            var studyTable = await _context.StudyTables.FindAsync(id);

            if (studyTable == null)
            {
                return NotFound();
            }

            return studyTable;
        }

        // PUT: api/StudyTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudyTable(int id, StudyTable studyTable)
        {
            if (id != studyTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(studyTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudyTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudyTable>> PostStudyTable(StudyTable studyTable)
        {
            _context.StudyTables.Add(studyTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudyTable", new { id = studyTable.Id }, studyTable);
        }

        // DELETE: api/StudyTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudyTable(int id)
        {
            var studyTable = await _context.StudyTables.FindAsync(id);
            if (studyTable == null)
            {
                return NotFound();
            }

            _context.StudyTables.Remove(studyTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudyTableExists(int id)
        {
            return _context.StudyTables.Any(e => e.Id == id);
        }
    }
}
