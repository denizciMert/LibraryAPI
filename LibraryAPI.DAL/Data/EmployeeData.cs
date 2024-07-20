using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
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

        public async Task<bool> IsRegistered(EmployeePost tPost)
        {
            var employees = await SelectAll();
            foreach (var employee in employees)
            {
                if (employee.ApplicationUser.IdentityNo == tPost.IdentityNo ||
                    employee.ApplicationUser.UserName == tPost.UserName ||
                    employee.ApplicationUser.Email == tPost.Email)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
