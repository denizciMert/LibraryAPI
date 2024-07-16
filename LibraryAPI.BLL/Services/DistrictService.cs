using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.DistrictDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class DistrictService : ILibraryServiceManager<DistrictGet,DistrictPost,District>
    {
        private readonly ApplicationDbContext _context;

        public DistrictService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<DistrictGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<District>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<DistrictGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<District>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<DistrictGet>> AddAsync(DistrictPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<DistrictGet>> UpdateAsync(int id, DistrictPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
