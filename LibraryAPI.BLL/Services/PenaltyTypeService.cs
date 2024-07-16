using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Services
{
    public class PenaltyTypeService : ILibraryServiceManager<PenaltyTypeGet,PenaltyTypePost,PenaltyType>
    {
        private readonly ApplicationDbContext _context;

        public PenaltyTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<PenaltyTypeGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<PenaltyType>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<PenaltyTypeGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<PenaltyType>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<PenaltyTypeGet>> AddAsync(PenaltyTypePost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<PenaltyTypeGet>> UpdateAsync(int id, PenaltyTypePost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
