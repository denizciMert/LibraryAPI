using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.BLL.Services; // Importing services from the Business Logic Layer
using LibraryAPI.BLL.Core; // Importing core functionalities from the Business Logic Layer
using LibraryAPI.DAL; // Importing Data Access Layer
using LibraryAPI.Entities.DTOs.AddressDTO; // Importing Address Data Transfer Objects
using LibraryAPI.Entities.DTOs.AuthorDTO; // Importing Author Data Transfer Objects
using LibraryAPI.Entities.DTOs.BookDTO; // Importing Book Data Transfer Objects
using LibraryAPI.Entities.DTOs.CategoryDTO; // Importing Category Data Transfer Objects
using LibraryAPI.Entities.DTOs.CityDTO; // Importing City Data Transfer Objects
using LibraryAPI.Entities.DTOs.CountryDTO; // Importing Country Data Transfer Objects
using LibraryAPI.Entities.DTOs.DepartmentDTO; // Importing Department Data Transfer Objects
using LibraryAPI.Entities.DTOs.DistrictDTO; // Importing District Data Transfer Objects
using LibraryAPI.Entities.DTOs.EmployeeDTO; // Importing Employee Data Transfer Objects
using LibraryAPI.Entities.DTOs.LanguageDTO; // Importing Language Data Transfer Objects
using LibraryAPI.Entities.DTOs.LoanDTO; // Importing Loan Data Transfer Objects
using LibraryAPI.Entities.DTOs.LocationDTO; // Importing Location Data Transfer Objects
using LibraryAPI.Entities.DTOs.MemberDTO; // Importing Member Data Transfer Objects
using LibraryAPI.Entities.DTOs.PenaltyDTO; // Importing Penalty Data Transfer Objects
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO; // Importing Penalty Type Data Transfer Objects
using LibraryAPI.Entities.DTOs.PublisherDTO; // Importing Publisher Data Transfer Objects
using LibraryAPI.Entities.DTOs.ReservationDTO; // Importing Reservation Data Transfer Objects
using LibraryAPI.Entities.DTOs.ShiftDTO; // Importing Shift Data Transfer Objects
using LibraryAPI.Entities.DTOs.StudyTableDTO; // Importing Study Table Data Transfer Objects
using LibraryAPI.Entities.DTOs.SubCategoryDTO; // Importing SubCategory Data Transfer Objects
using LibraryAPI.Entities.DTOs.TitleDTO; // Importing Title Data Transfer Objects
using LibraryAPI.Entities.Models; // Importing entity models
using Microsoft.AspNetCore.Identity; // Importing ASP.NET Core Identity for authentication and authorization
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core
using Microsoft.AspNetCore.Authentication.JwtBearer; // Importing JWT Bearer authentication
using Microsoft.IdentityModel.Tokens; // Importing Token validation parameters
using System.Text; // Importing text encoding utilities
using Microsoft.OpenApi.Models; // Importing OpenAPI/Swagger models
using System.Security.Claims; // Importing claims-based identity model
using System.Configuration; // Importing configuration utilities
using LibraryAPI.Entities.Filters; // Importing custom filters

namespace LibraryAPI.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); // Create a WebApplication builder
            var configuration = builder.Configuration; // Get configuration settings

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"),
                b => b.MigrationsAssembly("LibraryAPI.DAL"))); // Configure DbContext with SQL Server

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>() // Add Identity services
                .AddEntityFrameworkStores<ApplicationDbContext>() // Use EF Core stores
                .AddDefaultTokenProviders(); // Add default token providers

            builder.Services.AddAuthentication(); // Add authentication services
            builder.Services.AddAuthorization(); // Add authorization services

            // Add application services for various entities
            builder.Services.AddScoped<ILibraryServiceManager<AddressGet, AddressPost, Address>, AddressService>();
            builder.Services.AddScoped<ILibraryServiceManager<AuthorGet, AuthorPost, Author>, AuthorService>();
            builder.Services.AddScoped<ILibraryServiceManager<BookGet, BookPost, Book>, BookService>();
            builder.Services.AddScoped<ILibraryServiceManager<CategoryGet, CategoryPost, Category>, CategoryService>();
            builder.Services.AddScoped<ILibraryServiceManager<CityGet, CityPost, City>, CityService>();
            builder.Services.AddScoped<ILibraryServiceManager<CountryGet, CountryPost, Country>, CountryService>();
            builder.Services.AddScoped<ILibraryServiceManager<DepartmentGet, DepartmentPost, Department>, DepartmentService>();
            builder.Services.AddScoped<ILibraryServiceManager<DistrictGet, DistrictPost, District>, DistrictService>();
            builder.Services.AddScoped<ILibraryServiceManager<LanguageGet, LanguagePost, Language>, LanguageService>();
            builder.Services.AddScoped<ILibraryServiceManager<LoanGet, LoanPost, Loan>, LoanService>();
            builder.Services.AddScoped<ILibraryServiceManager<LocationGet, LocationPost, Location>, LocationService>();
            builder.Services.AddScoped<ILibraryServiceManager<PenaltyGet, PenaltyPost, Penalty>, PenaltyService>();
            builder.Services.AddScoped<ILibraryServiceManager<PenaltyTypeGet, PenaltyTypePost, PenaltyType>, PenaltyTypeService>();
            builder.Services.AddScoped<ILibraryServiceManager<PublisherGet, PublisherPost, Publisher>, PublisherService>();
            builder.Services.AddScoped<ILibraryServiceManager<ReservationGet, ReservationPost, Reservation>, ReservationService>();
            builder.Services.AddScoped<ILibraryServiceManager<ShiftGet, ShiftPost, Shift>, ShiftService>();
            builder.Services.AddScoped<ILibraryServiceManager<StudyTableGet, StudyTablePost, StudyTable>, StudyTableService>();
            builder.Services.AddScoped<ILibraryServiceManager<SubCategoryGet, SubCategoryPost, SubCategory>, SubCategoryService>();
            builder.Services.AddScoped<ILibraryServiceManager<TitleGet, TitlePost, Title>, TitleService>();
            builder.Services.AddScoped<ILibraryUserManager<MemberGet, MemberPost, Member>, MemberService>();
            builder.Services.AddScoped<ILibraryUserManager<EmployeeGet, EmployeePost, Employee>, EmployeeService>();
            builder.Services.AddScoped<ILibraryAccountManager, AccountService>();
            builder.Services.AddScoped<IFileUploadService, FileUploadService>();
            builder.Services.AddScoped<MailService>();

            // Configure JWT authentication
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not found in configuration."));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Set default authentication scheme
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Set default challenge scheme
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Validate the signing key
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Set the signing key
                    ValidateIssuer = true, // Validate the issuer
                    ValidIssuer = configuration["Jwt:Issuer"], // Set the valid issuer
                    ValidateAudience = true, // Validate the audience
                    ValidAudience = configuration["Jwt:Audience"], // Set the valid audience
                    ValidateLifetime = true, // Validate the token lifetime
                    ClockSkew = TimeSpan.Zero, // No clock skew
                    RoleClaimType = ClaimTypes.Role // Set the role claim type
                };
            });

            builder.Services.AddControllers(); // Add controllers to the container

            // Configure Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<SwaggerIgnoreFilter>(); // Apply custom schema filter
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token", // Description for Swagger UI
                    Name = "Authorization", // Name of the header
                    Type = SecuritySchemeType.Http, // HTTP type scheme
                    BearerFormat = "JWT", // JWT bearer format
                    Scheme = "Bearer" // Bearer scheme
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" // Reference to the bearer security scheme
                            }
                        },
                        new string[] { } // Empty scopes
                    }
                });
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                if (Convert.ToInt32(configuration["CORS:Count"]) == 0)
                {
                    options.AddDefaultPolicy(policy =>
                        policy.AllowAnyOrigin() // Allow any origin
                            .AllowAnyMethod() // Allow any method
                            .AllowAnyHeader()); // Allow any header
                }
                else
                {
                    var domains = configuration["CORS:Domains"] ?? throw new ConfigurationErrorsException("CORS:Domains not found in configuration.");
                    options.AddDefaultPolicy(policy =>
                        policy.WithOrigins(domains.Split(",")) // Allow specific origins
                            .AllowAnyMethod() // Allow any method
                            .AllowAnyHeader()); // Allow any header
                }
            });

            var app = builder.Build(); // Build the application

            // Migrate the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>(); // Get the DbContext
                    await context.Database.MigrateAsync(); // Apply migrations
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>(); // Get the logger
                    logger.LogError(ex, "An error occurred while migrating the database."); // Log the error
                }
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Enable Swagger in development
                app.UseSwaggerUI(); // Enable Swagger UI in development
            }

            app.UseStaticFiles(); // Serve static files
            app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
            app.UseAuthentication(); // Enable authentication middleware
            app.UseAuthorization(); // Enable authorization middleware
            app.UseCors(); // Enable CORS
            app.MapControllers(); // Map controller routes

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await RolesService.CreateRoles(services); // Create default roles
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>(); // Get the logger
                    logger.LogError(ex, "An error occurred while creating roles."); // Log the error
                }
            }

            await app.RunAsync(); // Run the application
        }
    }
}
