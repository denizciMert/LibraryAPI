using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILibraryServiceManager<CategoryGet,CategoryPost,Category> _libraryServiceManager;

        public CategoriesController(ILibraryServiceManager<CategoryGet, CategoryPost, Category> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Categories
        [Authorize]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<CategoryGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Categories/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CategoryGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, CategoryPost category)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, category);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<CategoryPost>> Post(CategoryPost category)
        {
            //UploadController uploadController = new UploadController();
            //var image = await uploadController.UploadImage(category.FileForm);
            var result = await _libraryServiceManager.AddAsync(category);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Categories/5
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
