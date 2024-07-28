using LibraryAPI.DAL;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryAPI.BLL.Core
{
    public class RolesService
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();

            foreach (var role in roles)
            {
                var roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            await CreateAdminUser(userManager, serviceProvider);
            await CreateBasePenalty(context, serviceProvider);
        }

        private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
        {
            var configuraiton = serviceProvider.GetRequiredService<IConfiguration>();
            
            string adminUserName = configuraiton["Sudo:Username"];
            string adminEmail = configuraiton["Sudo:Mail"];
            string adminPassword = configuraiton["Sudo:Password"];

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    UserRole = UserRole.Yönetici,
                    FirstName = "Admin",
                    LastName = "Poweruser",
                    PhoneNumber = "1234567890",
                    IdentityNo = "00000000000",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    DateOfRegister = DateTime.UtcNow,
                    Gender = Gender.Belirtilmedi,
                    Banned = false,
                    State = State.Eklendi,
                    UserImagePath = string.Empty
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRole.Yönetici.ToString());
                }
            }
        }

        private static async Task CreateBasePenalty(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var configuraiton = serviceProvider.GetRequiredService<IConfiguration>();

            string penaltyName = configuraiton["BasePenalty:Name"];
            int penaltyAmount = Convert.ToInt32(configuraiton["BasePenalty:Amount"]);

            var penalty = await context.PenaltyTypes.FirstOrDefaultAsync(x => x.PenaltyName == penaltyName);
            if (penalty==null)
            {
                var newPenalty = new PenaltyType
                {
                    AmountToPay = penaltyAmount,
                    PenaltyName = penaltyName,
                    CreationDateLog = DateTime.Now,
                    DeleteDateLog = null,
                    UpdateDateLog = null,
                    State = State.Eklendi
                };

                context.PenaltyTypes.Add(newPenalty);
                await context.SaveChangesAsync();
            }
        }
    }
}
