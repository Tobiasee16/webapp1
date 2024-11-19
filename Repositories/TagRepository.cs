using Webapp1.Models.Domain;

namespace Webapp1.Repositories
{
    public class TagRepository : ITagInterface
    {
        public Task<Tag> AddAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> UpdateAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}