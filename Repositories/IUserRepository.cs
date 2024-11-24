using Microsoft.AspNetCore.Identity;

namespace Webapp1.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}