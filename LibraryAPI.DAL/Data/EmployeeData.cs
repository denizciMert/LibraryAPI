using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class EmployeeData(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IQueryBase<Employee>
    {
        public async Task<List<Employee>> SelectAllFiltered()
        {
            return await context.Employees
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Department)
                .Include(x => x.Title)
                .Where(x => x.ApplicationUser.State != State.Silindi)
                .Where(x=>x.ApplicationUser.Banned==false)
                .ToListAsync();
        }

        public async Task<List<Employee>> SelectAll()
        {
            return await context.Employees
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
            return await context.Employees
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

        public async Task SaveUser(ApplicationUser user, string password)
        {
            await userManager.CreateAsync(user, password);
        }

        public async Task UpdateUserPassword(ApplicationUser user, string currentPassword, string newPassword)
        {
            await userManager.ChangePasswordAsync(user,currentPassword,newPassword);
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
        }

        public void AddToContext(Employee employee)
        {
            context.Employees.Add(employee);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
