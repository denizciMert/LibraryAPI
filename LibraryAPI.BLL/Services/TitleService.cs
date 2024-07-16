using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class TitleService : ILibraryServiceManager<TitleGet,TitlePost,Title>
    {
        private readonly ApplicationDbContext _context;
        public TitleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult<IEnumerable<TitleGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<Title>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TitleGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Title>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TitleGet>> AddAsync(TitlePost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TitleGet>> UpdateAsync(int id, TitlePost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
