using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class EmployeeData : IQueryBase<Employee>
    {
        private readonly ApplicationDbContext _context;

        public EmployeeData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> SelectAll()
        {
            return await _context.Employees
                .Include(x=>x.ApplicationUser).ThenInclude(x=>x.Country)
                .Include(x=>x.Department)
                .Include(x=>x.Title)
                .ToListAsync();
        }

        public async Task<Employee> SelectForEntity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> SelectForUser(string id)
        {
            return await _context.Employees
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Department)
                .Include(x => x.Title)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
