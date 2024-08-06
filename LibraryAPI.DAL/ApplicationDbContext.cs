using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Importing IdentityDbContext functionalities
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities
using LibraryAPI.Entities.Models; // Importing entity models

namespace LibraryAPI.DAL
{
    public class ApplicationDbContext : IdentityDbContext // Defining the ApplicationDbContext class that inherits from IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // Constructor accepting DbContextOptions and passing it to the base class
        {
        }

        // Defining DbSet properties for each entity in the database
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

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Overriding the OnModelCreating method to configure the model
        {
            base.OnModelCreating(modelBuilder); // Calling the base method

            // Configuring composite keys for join tables
            modelBuilder.Entity<AuthorBook>().HasKey(a => new { a.BooksId, a.AuthorsId }); // Configuring composite key for AuthorBook

            modelBuilder.Entity<BookLanguage>().HasKey(a => new { a.BooksId, a.LanguagesId }); // Configuring composite key for BookLanguage

            modelBuilder.Entity<BookSubCategory>().HasKey(a => new { a.BooksId, a.SubCategoriesId }); // Configuring composite key for BookSubCategory

            modelBuilder.Entity<BookCopy>().HasKey(x => new { x.BookId, x.CopyNo }); // Configuring composite key for BookCopy

            modelBuilder.Entity<BookRating>().HasKey(x => new { x.RatedBookId, x.RaterMemberId }); // Configuring composite key for BookRating

            // Configuring relationships and specifying delete behaviors
            modelBuilder.Entity<Address>()
                .HasOne(x => x.ApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Configuring relationship and delete behavior for Address

            modelBuilder.Entity<Penalty>()
                .HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.PenaltiedMembeId)
                .OnDelete(DeleteBehavior.Restrict); // Configuring relationship and delete behavior for Penalty

            modelBuilder.Entity<Loan>()
                .HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.LoanedMemberId)
                .OnDelete(DeleteBehavior.Restrict); // Configuring relationship and delete behavior for Loan

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict); // Configuring relationship and delete behavior for Reservation
        }
    }
}
