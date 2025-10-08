using SalonCoiffure.Data;

namespace SalonCoiffure.Tests.Tests
{
    public class ServiceTest
    {

        [Fact]
        public async Task Update_ServicePrix_MetAJourEnBase()
        {
            // Arrange
            Database.InitializeDatabase();
            using var context = new AppDbContext();
            var provider = new ServiceDataProvider(context);
            var services = await provider.GetAllAsync();
            var service = services.FirstOrDefault(s => s.Nom == "Coupe cheveux");

            Assert.NotNull(service); 

            decimal nouveauPrix = 35.00m;
            service.Prix = nouveauPrix;

            // Act
            await provider.UpdateAsync(service);

            // Recharger les donneés
            var servicesApresUpdate = await provider.GetAllAsync();
            var serviceMisAJour = servicesApresUpdate.FirstOrDefault(s => s.Id == service.Id);

            // Assert
            Assert.NotNull(serviceMisAJour);
            Assert.Equal(nouveauPrix, serviceMisAJour.Prix);
        }
    }
}
