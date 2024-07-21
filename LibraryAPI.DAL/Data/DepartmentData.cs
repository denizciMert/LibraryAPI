using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.DepartmentDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class DepartmentData(ApplicationDbContext context) : IQueryBase<Department>
    {
        public async Task<List<Department>> SelectAllFiltered()
        {
            return await context.Departments.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Department>> SelectAll()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> SelectForEntity(int id)
        {
            return await context.Departments.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Department> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(DepartmentPost tPost)
        {
            var departments = await SelectAll();
            foreach (var department in departments)
            {
                if (department.DepartmentName == tPost.DepartmentName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Department department)
        {
            context.Departments.Add(department);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
