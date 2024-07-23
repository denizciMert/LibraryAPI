using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.LanguageDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILibraryServiceManager<LanguageGet, LanguagePost, Language> _libraryServiceManager;

        public LanguagesController(ILibraryServiceManager<LanguageGet, LanguagePost, Language> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Languages
        [Authorize("Ziyaretçi")]
        [Authorize("Kullanıcı")]
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<LanguageGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize("Yönetici")]
        [HttpGet("GetData")]
        public async Task<ActionResult<IEnumerable<Language>>> GetAllData()
        {
            var result = await _libraryServiceManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Languages/5
        [Authorize("Kullanıcı")]
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<LanguageGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize("Yönetici")]
        [HttpGet("GetData/{id}")]
        public async Task<ActionResult<Language>> GetData(int id)
        {
            var result = await _libraryServiceManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, LanguagePost language)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, language);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<LanguagePost>> Post(LanguagePost language)
        {
            var result = await _libraryServiceManager.AddAsync(language);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Languages/5
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
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
