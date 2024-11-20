using Webapp1.Data;
using Microsoft.EntityFrameworkCore;
using Webapp1.Models.Domain;

namespace Webapp1.Repositories
{
    public class InnmeldingerRepository : IInnmeldingerRepository
    {
        private readonly Webapp1DbContext webapp1DbContext;
        public InnmeldingerRepository(Webapp1DbContext webapp1Dbcontext)
        {
            this.webapp1DbContext = webapp1Dbcontext;
        }

        public async Task<Innmelding> AddAsync(Innmelding innmelding)
        {
            await webapp1DbContext.AddAsync(innmelding);
            await webapp1DbContext.SaveChangesAsync();
            return innmelding;
        }

        public async Task<Innmelding?> DeleteAsync(Guid id)
        {
            var existingInnmelding = await webapp1DbContext.Innmeldinger.FindAsync(id);

            if (existingInnmelding != null)
            {
                webapp1DbContext.Innmeldinger.Remove(existingInnmelding);
                await webapp1DbContext.SaveChangesAsync();
                return existingInnmelding;
            }

            return null;
        }

        public async Task<IEnumerable<Innmelding>> GetAllAsync()
        {
            return await webapp1DbContext.Innmeldinger.Include(x => x. Tags).ToListAsync();
        }

        public async Task<Innmelding?> GetAsync(Guid id)
        {
            return await webapp1DbContext.Innmeldinger.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Innmelding?> UpdateAsync(Innmelding innmelding)
        {
            var existingInnmelding = await webapp1DbContext.Innmeldinger.Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == innmelding.Id);

            if (existingInnmelding != null)
            {
                existingInnmelding.Heading = innmelding.Heading;
                existingInnmelding.PageTitle = innmelding.PageTitle;
                existingInnmelding.Content = innmelding.Content;
                existingInnmelding.ShortDescription = innmelding.ShortDescription;
                existingInnmelding.FeaturedImageUrl = innmelding.FeaturedImageUrl;
                existingInnmelding.UrlHandle = innmelding.UrlHandle;
                existingInnmelding.PublishedDate = innmelding.PublishedDate;
                existingInnmelding.Author = innmelding.Author;
                existingInnmelding.Visible = innmelding.Visible;
                existingInnmelding.Tags = innmelding.Tags;

                await webapp1DbContext.SaveChangesAsync();

                return existingInnmelding;
            }
            return null;
        }
    }
}