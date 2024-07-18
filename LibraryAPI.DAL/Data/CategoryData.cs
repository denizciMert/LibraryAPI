﻿using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class CategoryData : IQueryBase<Category>
    {
        private readonly ApplicationDbContext _context;
        public CategoryData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> SelectAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> SelectForEntity(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> SelectForEntityName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == name);
        }

        public void AddToContext(Category category)
        {
            _context.Categories.Add(category);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}