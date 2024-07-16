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
    public class PenaltiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PenaltiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Penalties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Penalty>>> GetPenalties()
        {
            return await _context.Penalties.ToListAsync();
        }

        // GET: api/Penalties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Penalty>> GetPenalty(int id)
        {
            var penalty = await _context.Penalties.FindAsync(id);

            if (penalty == null)
            {
                return NotFound();
            }

            return penalty;
        }

        // PUT: api/Penalties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPenalty(int id, Penalty penalty)
        {
            if (id != penalty.Id)
            {
                return BadRequest();
            }

            _context.Entry(penalty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyExists(id))
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

        // POST: api/Penalties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Penalty>> PostPenalty(Penalty penalty)
        {
            _context.Penalties.Add(penalty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPenalty", new { id = penalty.Id }, penalty);
        }

        // DELETE: api/Penalties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePenalty(int id)
        {
            var penalty = await _context.Penalties.FindAsync(id);
            if (penalty == null)
            {
                return NotFound();
            }

            _context.Penalties.Remove(penalty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PenaltyExists(int id)
        {
            return _context.Penalties.Any(e => e.Id == id);
        }
    }
}
