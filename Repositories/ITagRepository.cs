using Webapp1.Models.Domain;

namespace Webapp1.Repositories
{
    public interface ITagRepository
    {
       Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null);


       Task<Tag?> GetByIdAsync(Guid id);

         Task<Tag> AddAsync(Tag tag);

            Task<Tag?> UpdateAsync(Tag tag);

            Task<Tag?> DeleteAsync(Guid id);

       
    }
}