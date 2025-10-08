using SalonCoiffure.Data;
using SalonCoiffure.Model;

namespace SalonCoiffure.Tests.Tests
{
    public class CustomerTest
    {
        [Fact]
        public void AddCustomer_AjouteClientDansBaseDeDonnees()
        {
            //Arrange
            Database.InitializeDatabase();

            var nouveauClient = new Customer
            {
                Nom = "Durand",
                Telephone = "1234567890",
                Email = "durand@example.com",
                Adresse = "123 rue Principale"
            };

            // Act 
            using (var context = new AppDbContext())
            {
                context.Customers.Add(nouveauClient);
                context.SaveChanges();
            }

            // Assert 
            using (var context = new AppDbContext())
            {
                var clientDansBd = context.Customers.FirstOrDefault(
                    c => c.Nom == "Durand" && c.Telephone == "1234567890");

                
                Assert.Equal("durand@example.com", clientDansBd.Email);
                Assert.Equal("123 rue Principale", clientDansBd.Adresse);

            }
        }
    }
}
