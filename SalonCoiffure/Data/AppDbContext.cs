using Microsoft.EntityFrameworkCore;
using SalonCoiffure.Model;

namespace SalonCoiffure.Data
{
    public class AppDbContext : DbContext
    {
     

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=salonCoiffure.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Factures)
                .WithOne(f => f.Customer)
                .HasForeignKey(f => f.CustomerId);


            modelBuilder.Entity<Facture>()
                .HasMany(f => f.Services)
                .WithMany(s => s.Factures)
                .UsingEntity<Dictionary<string, object>>(
                        "FactureService",  // Nom de la table de jointure en arrière-plan
                        j => j.HasOne<Service>().WithMany().HasForeignKey("ServiceId"),
                        j => j.HasOne<Facture>().WithMany().HasForeignKey("FactureId")
    );


            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Paiement)
                .WithOne(p => p.Facture)
                .HasForeignKey<Paiement>(p => p.FactureId);
        }
    }
}
