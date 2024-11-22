using Microsoft.EntityFrameworkCore;
using Webapp1.Models.Domain;

namespace   Webapp1.Data
{
    public class Webapp1DbContext : DbContext
    {
        public Webapp1DbContext(DbContextOptions<Webapp1DbContext> options) : base(options)
        {
        }

        public DbSet<Innmelding> Innmeldinger { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
