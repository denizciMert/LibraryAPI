using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.CityDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ILibraryServiceManager<CityGet, CityPost, City> _libraryServiceManager;
        public CitiesController(ILibraryServiceManager<CityGet, CityPost, City> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Cities
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<CityGet>>> GetAll()
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
        public async Task<ActionResult<IEnumerable<City>>> GetAllData()
        {
            var result = await _libraryServiceManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Cities/5
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CityGet>> Get(int id)
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
        public async Task<ActionResult<City>> GetData(int id)
        {
            var result = await _libraryServiceManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, CityPost city)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, city);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<CityPost>> Post(CityPost city)
        {
            var result = await _libraryServiceManager.AddAsync(city);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Cities/5
        [Authorize("Kullanıcı")]
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
