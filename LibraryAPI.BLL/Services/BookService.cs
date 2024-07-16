using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.BookDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.BLL.Services
{
    public class BookService : ILibraryServiceManager<BookGet,BookPost,Book>
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<BookGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<Book>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BookGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Book>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BookGet>> AddAsync(BookPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BookGet>> UpdateAsync(int id, BookPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
