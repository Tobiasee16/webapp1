using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Webapp1.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed Roles (Admin, User, SuperAdmin)

            var adminRoleId = "0E69165A-A912-4BB6-9939-63678CDDE051";
            var superAdminRoleId = "F1045A6A-29ED-4B90-84C4-A6AC69E99C9B";
            var userRoleId = "728B812A-B4BC-40B9-9CB8-275AB5FBDE04";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = "userRoleId",
                    ConcurrencyStamp = "userRoleId"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdminUser
            var superAdminId = "1316EE9B-C58E-4D1C-ACB2-AF681EFA267B";
            var superAdminUser = new IdentityUser
            {
                UserName = "engedaltobias@gmail.com",
                Email = "engedaltobias@gmail.com",
                NormalizedEmail = "engedaltobias@gmail.com".ToUpper(),
                NormalizedUserName = "engedaltobias@gmail.com".ToUpper(),
                Id = superAdminId

            //Add All roles to SuperAdmin
        }

    }
}