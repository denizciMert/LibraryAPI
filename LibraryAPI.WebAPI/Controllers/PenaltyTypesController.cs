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
    public class PenaltyTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PenaltyTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PenaltyTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PenaltyType>>> GetPenaltyTypes()
        {
            return await _context.PenaltyTypes.ToListAsync();
        }

        // GET: api/PenaltyTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PenaltyType>> GetPenaltyType(int id)
        {
            var penaltyType = await _context.PenaltyTypes.FindAsync(id);

            if (penaltyType == null)
            {
                return NotFound();
            }

            return penaltyType;
        }

        // PUT: api/PenaltyTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPenaltyType(int id, PenaltyType penaltyType)
        {
            if (id != penaltyType.Id)
            {
                return BadRequest();
            }

            _context.Entry(penaltyType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyTypeExists(id))
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

        // POST: api/PenaltyTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PenaltyType>> PostPenaltyType(PenaltyType penaltyType)
        {
            _context.PenaltyTypes.Add(penaltyType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPenaltyType", new { id = penaltyType.Id }, penaltyType);
        }

        // DELETE: api/PenaltyTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePenaltyType(int id)
        {
            var penaltyType = await _context.PenaltyTypes.FindAsync(id);
            if (penaltyType == null)
            {
                return NotFound();
            }

            _context.PenaltyTypes.Remove(penaltyType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PenaltyTypeExists(int id)
        {
            return _context.PenaltyTypes.Any(e => e.Id == id);
        }
    }
}
