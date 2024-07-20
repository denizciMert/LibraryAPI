using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Entities.Models;


namespace LibraryAPI.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<AuthorBook>? AuthorBooks { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<BookCopy>? BookCopies { get; set; }
        public DbSet<BookLanguage>? BookLanguages { get; set; }
        public DbSet<BookSubCategory>? BookSubCategories { get; set; }
        public DbSet<BookRating>? BookRatings { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<City>? Cities { get; set; }
        public DbSet<Country>? Countries { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<District>? Districts { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Language>? Languages { get; set; }
        public DbSet<Loan>? Loans { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<Penalty>? Penalties { get; set; }
        public DbSet<PenaltyType>? PenaltyTypes { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Shift>? Shifts { get; set; }
        public DbSet<StudyTable>? StudyTables { get; set; }
        public DbSet<SubCategory>? SubCategories { get; set; }
        public DbSet<Title>? Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorBook>().HasKey(a => new { a.BooksId, a.AuthorsId });

            modelBuilder.Entity<BookLanguage>().HasKey(a => new { a.BooksId, a.LanguagesId });

            modelBuilder.Entity<BookSubCategory>().HasKey(a => new { a.BooksId, a.SubCategoriesId });

            modelBuilder.Entity<BookCopy>().HasKey(x => new { x.BookId, x.CopyNo });

            modelBuilder.Entity<BookRating>().HasKey(x => new { x.RatedBookId, x.RaterMemberId });

            modelBuilder.Entity<Address>().HasOne(x => x.ApplicationUser).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Penalty>().HasOne(x => x.Member).WithMany().HasForeignKey(x => x.PenaltiedMembeId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Loan>().HasOne(x => x.Member).WithMany().HasForeignKey(x => x.LoanedMemberId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>().HasOne(x => x.Member).WithMany().HasForeignKey(x => x.MemberId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
