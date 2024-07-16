using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.LanguageDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class LanguageService : ILibraryServiceManager<LanguageGet,LanguagePost,Language>
    {
        private readonly ApplicationDbContext _context;
        public LanguageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<LanguageGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<Language>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<LanguageGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Language>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<LanguageGet>> AddAsync(LanguagePost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<LanguageGet>> UpdateAsync(int id, LanguagePost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
