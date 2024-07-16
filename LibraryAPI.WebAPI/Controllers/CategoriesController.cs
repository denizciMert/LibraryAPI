using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILibraryServiceManager<CategoryGet,CategoryPost,Category> _libraryServiceManager;
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context, ILibraryServiceManager<CategoryGet, CategoryPost, Category> libraryServiceManager)
        {
            _context = context;
            _libraryServiceManager = libraryServiceManager;
        }

        // GET: api/Categories
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("GetData")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesData()
        {
            var result = await _libraryServiceManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Categories/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("GetData/{id}")]
        public async Task<ActionResult<Category>> GetCategoryData(int id)
        {
            var result = await _libraryServiceManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryPost category)
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
        [HttpPost("Post")]
        public async Task<ActionResult<CategoryPost>> PostCategory(CategoryPost category)
        {
            var result = await _libraryServiceManager.AddAsync(category);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Categories/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
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
