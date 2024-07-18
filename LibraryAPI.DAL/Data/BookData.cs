﻿using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class BookData : IQueryBase<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> SelectAll()
        {
            return await _context.Books
                .Include(x => x.AuthorBooks).ThenInclude(x => x.Author)
                .Include(x => x.BookLanguages).ThenInclude(x => x.Language)
                .Include(x => x.BookLanguages).ThenInclude(x => x.Language)
                .Include(x=>x.BookSubCategories).ThenInclude(x=>x.SubCategory)
                .Include(x=>x.Publisher)
                .Include(x => x.Location).ToListAsync();
        }

        public async Task<Book> SelectForEntity(int id)
        {
            return await _context.Books
                .Include(x => x.AuthorBooks).ThenInclude(x => x.Author)
                .Include(x => x.BookLanguages).ThenInclude(x => x.Language)
                .Include(x => x.BookLanguages).ThenInclude(x => x.Language)
                .Include(x => x.BookSubCategories).ThenInclude(x => x.SubCategory)
                .Include(x => x.Publisher)
                .Include(x => x.Location).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Book> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}