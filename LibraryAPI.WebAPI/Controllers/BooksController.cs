﻿using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.BookDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryServiceManager<BookGet, BookPost, Book> _libraryServiceManager;
        private readonly IFileUploadService _fileUploadService;

        public BooksController(ILibraryServiceManager<BookGet, BookPost, Book> libraryServiceManager, IFileUploadService fileUploadService)
        {
            _libraryServiceManager = libraryServiceManager;
            _fileUploadService = fileUploadService;
        }

        // GET: api/Books
        [Authorize]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<BookGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Books/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<BookGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, BookPost book)
        {
            if (book.FileForm != null)
            {
                var imagePath = await _fileUploadService.UploadImage(book.FileForm);
                if (!imagePath.Success)
                {
                    return BadRequest(imagePath.ErrorMessage);
                }
                book.ImagePath = imagePath.Data;
            }

            var result = await _libraryServiceManager.UpdateAsync(id, book);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<BookLanguage>> Post(BookPost book)
        {
            if (book.FileForm != null)
            {
                var imagePath = await _fileUploadService.UploadImage(book.FileForm);
                if (!imagePath.Success)
                {
                    return BadRequest(imagePath.ErrorMessage);
                }
                book.ImagePath = imagePath.Data;
            }
            else
            {
                book.ImagePath = null;
            }

            var result = await _libraryServiceManager.AddAsync(book);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Books/5
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
