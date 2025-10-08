using SalonCoiffure.Model;
using System.Threading.Tasks;

namespace SalonCoiffure.Data
{
    public interface IPaiementDataProvider
    {
        Task AddAsync(Paiement paiement);
        Task<Paiement> GetByFactureIdAsync(int factureId);
    }
}
