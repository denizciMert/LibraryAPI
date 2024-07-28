using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.TitleDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly ILibraryServiceManager<TitleGet, TitlePost, Title> _libraryServiceManager;

        public TitlesController(ILibraryServiceManager<TitleGet, TitlePost, Title> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Titles
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<TitleGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Titles/5
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TitleGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Titles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, TitlePost title)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, title);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Titles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<TitlePost>> Post(TitlePost title)
        {
            var result = await _libraryServiceManager.AddAsync(title);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Titles/5
        [Authorize(Roles = "Yönetici")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
