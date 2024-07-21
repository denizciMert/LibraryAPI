using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.DTOs.PublisherDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DAL.Data
{
    public class PublisherData(ApplicationDbContext context) : IQueryBase<Publisher>
    {
        public async Task<List<Publisher>> SelectAllFiltered()
        {
            return await context.Publishers.Where(x=>x.State!=State.Silindi).ToListAsync();
        }

        public async Task<List<Publisher>> SelectAll()
        {
            return await context.Publishers.ToListAsync();
        }

        public async Task<Publisher> SelectForEntity(int id)
        {
            return await context.Publishers.FirstOrDefaultAsync(x => x.Id == id);
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
            context.Publishers.Add(publisher);
        }

        public async Task SaveContext()
        {
            await context.SaveChangesAsync();
        }
    }
}
