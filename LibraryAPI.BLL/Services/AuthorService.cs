using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class AuthorService : ILibraryServiceManager<AuthorGet,AuthorPost,Author>
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<AuthorGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<Author>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AuthorGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Author>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AuthorGet>> AddAsync(AuthorPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AuthorGet>> UpdateAsync(int id, AuthorPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
