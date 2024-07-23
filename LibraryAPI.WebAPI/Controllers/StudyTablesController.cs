using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.StudyTableDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class StudyTablesController : ControllerBase
    {
        private readonly ILibraryServiceManager<StudyTableGet, StudyTablePost, StudyTable> _libraryServiceManager;

        public StudyTablesController(ILibraryServiceManager<StudyTableGet, StudyTablePost, StudyTable> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/StudyTables
        [Authorize("Kullanıcı")]
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<StudyTableGet>>> GetAll()
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
        public async Task<ActionResult<IEnumerable<StudyTable>>> GetAllData()
        {
            var result = await _libraryServiceManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/StudyTables/5
        [Authorize("Kullanıcı")]
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<StudyTableGet>> Get(int id)
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
        public async Task<ActionResult<StudyTable>> GetData(int id)
        {
            var result = await _libraryServiceManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/StudyTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, StudyTablePost studyTable)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, studyTable);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/StudyTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<StudyTablePost>> Post(StudyTablePost studyTable)
        {
            var result = await _libraryServiceManager.AddAsync(studyTable);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/StudyTables/5
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
