using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.PublisherDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ILibraryServiceManager<PublisherGet, PublisherPost, Publisher> _libraryServiceManager;

        public PublishersController(ILibraryServiceManager<PublisherGet, PublisherPost, Publisher> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Publishers
        [Authorize("Ziyaretçi")]
        [Authorize("Kullanıcı")]
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<PublisherGet>>> GetAll()
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
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAllData()
        {
            var result = await _libraryServiceManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Publishers/5
        [Authorize("Kullanıcı")]
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<PublisherGet>> Get(int id)
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
        public async Task<ActionResult<Publisher>> GetData(int id)
        {
            var result = await _libraryServiceManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, PublisherPost publisher)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, publisher);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Çalışan")]
        [Authorize("Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<PublisherPost>> Post(PublisherPost publisher)
        {
            var result = await _libraryServiceManager.AddAsync(publisher);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Publishers/5
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
