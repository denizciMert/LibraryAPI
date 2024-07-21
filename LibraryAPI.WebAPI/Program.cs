using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Services;
using LibraryAPI.DAL;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.AuthorDTO;
using LibraryAPI.Entities.DTOs.BookDTO;
using LibraryAPI.Entities.DTOs.CategoryDTO;
using LibraryAPI.Entities.DTOs.CityDTO;
using LibraryAPI.Entities.DTOs.CountryDTO;
using LibraryAPI.Entities.DTOs.DepartmentDTO;
using LibraryAPI.Entities.DTOs.DistrictDTO;
using LibraryAPI.Entities.DTOs.EmployeeDTO;
using LibraryAPI.Entities.DTOs.LanguageDTO;
using LibraryAPI.Entities.DTOs.LoanDTO;
using LibraryAPI.Entities.DTOs.LocationDTO;
using LibraryAPI.Entities.DTOs.MemberDTO;
using LibraryAPI.Entities.DTOs.PenaltyDTO;
using LibraryAPI.Entities.DTOs.PenaltyTypeDTO;
using LibraryAPI.Entities.DTOs.PublisherDTO;
using LibraryAPI.Entities.DTOs.ReservationDTO;
using LibraryAPI.Entities.DTOs.ShiftDTO;
using LibraryAPI.Entities.DTOs.StudyTableDTO;
using LibraryAPI.Entities.DTOs.SubCategoryDTO;
using LibraryAPI.Entities.DTOs.TitleDTO;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"),
                    b=>b.MigrationsAssembly("LibraryAPI.DAL")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
