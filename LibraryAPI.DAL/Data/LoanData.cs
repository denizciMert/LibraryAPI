﻿using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.AddressDTO;
using LibraryAPI.Entities.DTOs.LoanDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class LoanData(ApplicationDbContext context) : IQueryBase<Loan>
    {
        public async Task<List<Loan>> SelectAllFiltered()
        {
            return await context.Loans
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Book)
                .Where(x=>x.State!=State.Silindi)
                .ToListAsync();
        }

        public async Task<List<Loan>> SelectAll()
        {
            return await context.Loans
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Book)
                .ToListAsync();
        }

        public async Task<Loan> SelectForEntity(int id)
        {
            return await context.Loans
                .Include(x => x.Member).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Book)
                .FirstOrDefaultAsync(x => x.Book.Id == id);
        }

        public async Task<Loan> SelectForUser(string id)
        {
            return await context.Loans
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
            context.Loans.Add(loan);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
