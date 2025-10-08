using Microsoft.EntityFrameworkCore;
using SalonCoiffure.Model;
using System.Threading.Tasks;

namespace SalonCoiffure.Data
{
    public class PaiementDataProvider : IPaiementDataProvider
    {
        private readonly AppDbContext _context;

        public PaiementDataProvider(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();
        }

        public async Task<Paiement> GetByFactureIdAsync(int factureId)
        {
            return await _context.Paiements
                .FirstOrDefaultAsync(p => p.FactureId == factureId);
        }
    }
}
