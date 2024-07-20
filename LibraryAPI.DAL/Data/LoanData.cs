using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.LoanDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class LoanData : IQueryBase<Loan>
    {
        private readonly ApplicationDbContext _context;
        public LoanData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> SelectAll()
        {
            return await _context.Loans
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Book)
                .ToListAsync();
        }

        public async Task<Loan> SelectForEntity(int id)
        {
            return await _context.Loans
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Book)
                .FirstOrDefaultAsync(x => x.Book.Id == id);
        }

        public async Task<Loan> SelectForUser(string id)
        {
            return await _context.Loans
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Book)
                .FirstOrDefaultAsync(x => x.LoanedMemberId == id);
        }

        public async Task<bool> IsRegistered(LoanPost tPost)
        {
            var loans = await SelectAll();
            foreach (var loan in loans)
            {
                if (loan.BookId == tPost.BookId &&
                    loan.LoanedMemberId == tPost.MemberId &&
                    loan.Active == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Loan loan)
        {
            _context.Loans.Add(loan);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
