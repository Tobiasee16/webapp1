using Webapp1.Models.Domain;
using Webapp1.Data;
using Microsoft.EntityFrameworkCore;

namespace Webapp1.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly Webapp1DbContext webapp1DbContext;

        public TagRepository(Webapp1DbContext webapp1DbContext) 
        {
            this.webapp1DbContext = webapp1DbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await webapp1DbContext.Tags.AddAsync(tag);
            await webapp1DbContext.SaveChangesAsync();
            return tag; 
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await webapp1DbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                webapp1DbContext.Tags.Remove(existingTag);
                await webapp1DbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery,
            string? sortBy,
            string? sortDirection)
        {
            var query = webapp1DbContext.Tags.AsQueryable();
            //Filtering
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || x.DisplayName.Contains(searchQuery));
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);
                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name): query.OrderBy(x => x.Name);
                }
                
                if(string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName): query.OrderBy(x => x.DisplayName);
                }
               
            }
            //Pagination
            return await query.ToListAsync();
           //return await webapp1DbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return webapp1DbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await webapp1DbContext.Tags.FindAsync(id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await webapp1DbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await webapp1DbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}