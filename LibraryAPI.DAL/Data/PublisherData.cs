using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.PublisherDTO;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class PublisherData : IQueryBase<Publisher>
    {
        private readonly ApplicationDbContext _context;

        public PublisherData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> SelectAll()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher> SelectForEntity(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Publisher> SelectForUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRegistered(PublisherPost tPost)
        {
            var publishers = await SelectAll();
            foreach (var publisher in publishers)
            {
                if (publisher.PublisherName == tPost.PublisherName)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToContext(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
