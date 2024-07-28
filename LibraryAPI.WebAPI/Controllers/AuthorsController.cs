using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILibraryServiceManager<AuthorGet, AuthorPost, Author> _libraryServiceManager;

        public AuthorsController(ILibraryServiceManager<AuthorGet, AuthorPost, Author> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Authors
        [Authorize]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<AuthorGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }


        // GET: api/Authors/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<AuthorGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, AuthorPost author)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, author);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<AuthorPost>> Post(AuthorPost author)
        {
            var result = await _libraryServiceManager.AddAsync(author);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Authors/5
        [Authorize(Roles = "Çalışan,Yönetici")]
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
