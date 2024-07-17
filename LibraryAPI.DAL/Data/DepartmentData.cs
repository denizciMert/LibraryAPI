using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class DepartmentData : IQueryBase<Department>
    {
        private readonly ApplicationDbContext _context;
        public DepartmentData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> SelectAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> SelectForEntity(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Department> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
