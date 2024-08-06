using LibraryAPI.DAL; // Importing the data access layer
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.AspNetCore.Identity; // Importing ASP.NET Core Identity functionalities
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities
using Microsoft.Extensions.Configuration; // Importing configuration functionalities
using Microsoft.Extensions.DependencyInjection; // Importing dependency injection functionalities

namespace LibraryAPI.BLL.Core
{
    // Service class for handling roles and initial setup
    public class RolesService
    {
        // Method to create roles and initialize the admin user and base penalty
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); // Getting the role manager from the service provider
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(); // Getting the user manager from the service provider
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>(); // Getting the application database context from the service provider

            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>(); // Getting all roles from the UserRole enum

            foreach (var role in roles)
            {
                var roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName)) // Checking if the role already exists
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName)); // Creating the role if it does not exist
                }
            }
            await CreateAdminUser(userManager, serviceProvider); // Creating the admin user
            await CreateBasePenalty(context, serviceProvider); // Creating the base penalty
        }

        // Method to create the admin user
        private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>(); // Getting the configuration from the service provider

            string? adminUserName = configuration["Sudo:Username"]; // Getting the admin username from the configuration
            string? adminEmail = configuration["Sudo:Mail"]; // Getting the admin email from the configuration
            string? adminPassword = configuration["Sudo:Password"]; // Getting the admin password from the configuration

            if (adminEmail != null)
            {
                var adminUser = await userManager.FindByEmailAsync(adminEmail); // Finding the admin user by email
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

                    if (adminPassword != null)
                    {
                        var result = await userManager.CreateAsync(adminUser, adminPassword); // Creating the admin user with the specified password
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser, UserRole.Yönetici.ToString()); // Adding the admin user to the "Yönetici" role
                        }
                    }
                }
            }
        }

        // Method to create the base penalty
        private static async Task CreateBasePenalty(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>(); // Getting the configuration from the service provider

            string? penaltyName = configuration["BasePenalty:Name"]; // Getting the base penalty name from the configuration
            int penaltyAmount = Convert.ToInt32(configuration["BasePenalty:Amount"]); // Getting the base penalty amount from the configuration

            if (context.PenaltyTypes != null)
            {
                var penalty = await context.PenaltyTypes.FirstOrDefaultAsync(x => x.PenaltyName == penaltyName); // Finding the penalty by name
                if (penalty == null)
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

                    context.PenaltyTypes.Add(newPenalty); // Adding the new penalty to the context
                    await context.SaveChangesAsync(); // Saving changes to the context
                }
            }
        }
    }
}
