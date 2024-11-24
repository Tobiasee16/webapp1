using Microsoft.AspNetCore.Identity;
using Webapp1.Data;
using Microsoft.EntityFrameworkCore;

namespace Webapp1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;
        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
       
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();
            var superAdminUser = await authDbContext.Users
            .FirstOrDefaultAsync(x => x.Email == "superadmin@kartverket.com");

            if (superAdminUser is not null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
       
    }
}