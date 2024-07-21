﻿using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILibraryUserManager<EmployeeGet,EmployeePost,Employee> _libraryUserManager;

        public EmployeesController(ILibraryUserManager<EmployeeGet, EmployeePost, Employee> libraryUserManager)
        {
            _libraryUserManager = libraryUserManager;
        }

        // GET: api/Employees
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
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(string id, EmployeePost employee)
        {
            var result = await _libraryUserManager.UpdateAsync(id, employee);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Post")]
        public async Task<ActionResult<EmployeePost>> Post(EmployeePost employee)
        {
            var result = await _libraryUserManager.AddAsync(employee);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Employees/5
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