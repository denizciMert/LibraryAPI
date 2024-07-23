using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILibraryUserManager<EmployeeGet,EmployeePost,Employee> _libraryUserManager;
        private readonly IFileUploadService _fileUploadService;

        public EmployeesController(ILibraryUserManager<EmployeeGet, EmployeePost, Employee> libraryUserManager, IFileUploadService fileUploadService)
        {
            _libraryUserManager = libraryUserManager;
            _fileUploadService = fileUploadService;
        }

        // GET: api/Employees
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<EmployeeGet>>> GetAll()
        {
            var result = await _libraryUserManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize(Roles = "Yönetici")]
        [HttpGet("GetData")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllData()
        {
            var result = await _libraryUserManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Employees/5
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<EmployeeGet>> Get(string id)
        {
            var result = await _libraryUserManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize(Roles = "Yönetici")]
        [HttpGet("GetData/{id}")]
        public async Task<ActionResult<Employee>> GetData(string id)
        {
            var result = await _libraryUserManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(string id, EmployeePost employee)
        {
            if (employee.FileForm != null)
            {
                var imagePath = await _fileUploadService.UploadImage(employee.FileForm);
                if (!imagePath.Success)
                {
                    return BadRequest(imagePath.ErrorMessage);
                }
                employee.UserImagePath = imagePath.Data;
            }
            var result = await _libraryUserManager.UpdateAsync(id, employee);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<EmployeePost>> Post(EmployeePost employee)
        {
            if (employee.FileForm!=null)
            {
                var imagePath= await _fileUploadService.UploadImage(employee.FileForm);
                if (!imagePath.Success)
                {
                    return BadRequest(imagePath.ErrorMessage);
                }
                employee.UserImagePath = imagePath.Data;
            }
            else
            {
                employee.UserImagePath = null;
            }

            var result = await _libraryUserManager.AddAsync(employee);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        // DELETE: api/Employees/5
        [Authorize(Roles = "Yönetici")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _libraryUserManager.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
