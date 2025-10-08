using SalonCoiffure.Model;

namespace SalonCoiffure.Data
{
    public interface IFactureDataProvider
    {
        Task AddAsync(Facture facture);
        Task<IEnumerable<Facture>> GetAllAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(Facture facture);

    }

}
