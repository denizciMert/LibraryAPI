using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities
using Microsoft.EntityFrameworkCore.Design; // Importing design-time DbContext factory functionalities
using Microsoft.Extensions.Configuration; // Importing configuration functionalities

namespace LibraryAPI.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> // Defining a factory class for design-time DbContext creation
    {
        public ApplicationDbContext CreateDbContext(string[] args) // Method to create a DbContext instance
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>(); // Creating a DbContextOptionsBuilder instance for ApplicationDbContext

            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); // Retrieving the ASP.NET Core environment variable

            var config = new ConfigurationBuilder() // Creating a configuration builder
                .SetBasePath(Directory.GetCurrentDirectory()) // Setting the base path to the current directory
                .AddJsonFile("appsettings.json") // Adding the appsettings.json file to the configuration
                .Build(); // Building the configuration

            var connectionString = config.GetConnectionString("ApplicationDbContext"); // Retrieving the connection string for ApplicationDbContext

            optionsBuilder.UseSqlServer(connectionString); // Configuring the DbContext to use SQL Server with the connection string

            return new ApplicationDbContext(optionsBuilder.Options); // Creating and returning a new ApplicationDbContext instance with the configured options
        }
    }
}