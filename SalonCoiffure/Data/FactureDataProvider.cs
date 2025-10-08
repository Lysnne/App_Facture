using Microsoft.EntityFrameworkCore;
using SalonCoiffure.Model;

namespace SalonCoiffure.Data
{
    public class FactureDataProvider : IFactureDataProvider
    {
        private readonly AppDbContext _context;

        public FactureDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Facture>> GetAllAsync()
        {
            using var context = new AppDbContext();
            return await context.Factures
                .Include(f => f.Customer)
                .Include(f => f.Services)
                .ToListAsync();
        }
        public async Task AddAsync(Facture facture)
        {
            using var context = new AppDbContext();
            context.Factures.Add(facture);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            using var context = new AppDbContext();
            var facture = await context.Factures
                .Include(f => f.Services)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (facture != null)
            {

                facture.Services.Clear();

                context.Factures.Remove(facture);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Facture updatedFacture)
        {
            using var context = new AppDbContext();
            var existing = await context.Factures
                .Include(f => f.Services)
                .FirstOrDefaultAsync(f => f.Id == updatedFacture.Id);

            if (existing != null)
            {
                existing.Date = updatedFacture.Date;
                existing.PrixTotal = updatedFacture.PrixTotal;
                existing.CustomerId = updatedFacture.CustomerId;

                existing.Services.Clear();
                foreach (var s in updatedFacture.Services)
                {
                    var service = await context.Services.FindAsync(s.Id);
                    if (service != null)
                        existing.Services.Add(service);
                }

                await context.SaveChangesAsync();
            }
        }

     
    }

}
