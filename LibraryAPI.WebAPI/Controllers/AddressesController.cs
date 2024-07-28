using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ILibraryServiceManager<AddressGet, AddressPost, Address> _libraryServiceManager;

        public AddressesController(ILibraryServiceManager<AddressGet, AddressPost, Address> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Addresses
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<AddressGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }


        // GET: api/Addresses/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<AddressGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, AddressPost address)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, address);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<AddressPost>> Post(AddressPost address)
        {
            var result = await _libraryServiceManager.AddAsync(address);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Addresses/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
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
