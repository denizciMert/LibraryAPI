using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.MemberDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class MemberData(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IQueryBase<Member>
    {
        public async Task<List<Member>> SelectAllFiltered()
        {
            return await context.Members
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .Where(x=>x.ApplicationUser.Banned==false)
                .Where(x=>x.ApplicationUser.State!=State.Silindi)
                .ToListAsync();
        }

        public async Task<List<Member>> SelectAll()
        {
            return await context.Members
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .ToListAsync();
        }

        public async Task<Member> SelectForEntity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> SelectForUser(string id)
        {
            return await context.Members
                .Include(x => x.ApplicationUser).ThenInclude(x => x.Country)
                .Include(x => x.Loans)
                .Include(x => x.Penalties)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> IsRegistered(MemberPost tPost)
        {
            var members = await SelectAll();
            foreach (var member in members)
            {
                if (member.ApplicationUser.Email == tPost.Email &&
                    member.ApplicationUser.IdentityNo == tPost.IdentityNo &&
                    member.ApplicationUser.UserName == tPost.UserName)
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
            await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
        }

        public void AddToContext(Member member)
        {
            context.Members.Add(member);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
