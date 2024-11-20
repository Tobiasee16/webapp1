using Webapp1.Models.Domain;

namespace Webapp1.Repositories
{
    public interface IInnmeldingerRepository
    {
        Task<IEnumerable<Innmelding>> GetAllAsync();
        Task<Innmelding?> GetAsync(Guid id);
        Task<Innmelding> AddAsync(Innmelding innmelding);
        Task<Innmelding?> UpdateAsync(Innmelding innmelding);
        Task<Innmelding?> DeleteAsync(Guid id);
    }
}