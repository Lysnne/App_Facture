using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalonCoiffure.Model;

namespace SalonCoiffure.Data
{
    public class ServiceDataProvider : IServiceDataProvider
    {
        private readonly AppDbContext _context;

        public ServiceDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            using var context = new AppDbContext();
            var services = await context.Services.ToListAsync();

            Console.WriteLine($"Nombre de services récupérés : {services.Count}");

            return services;
        }

        public async Task AddAsync(Service service)
        {
            using var context = new AppDbContext();
            context.Services.Add(service);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            using var context = new AppDbContext();
            context.Services.Update(service);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Service service)
        {
            using var context = new AppDbContext();
            context.Services.Remove(service);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAvailableServicesAsync()
        {
            using var context = new AppDbContext();
            return await context.Services.ToListAsync();
        }

    }
}
