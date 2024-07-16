using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.DepartmentDTO;
using LibraryAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAPI.BLL.Core;
using LibraryAPI.DAL;

namespace LibraryAPI.BLL.Services
{
    public class DepartmentService : ILibraryServiceManager<DepartmentGet,DepartmentPost,Department>
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<IEnumerable<DepartmentGet>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<Department>>> GetAllWithDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<DepartmentGet>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Department>> GetWithDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<DepartmentGet>> AddAsync(DepartmentPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<DepartmentGet>> UpdateAsync(int id, DepartmentPost tPost)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
