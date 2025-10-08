using SalonCoiffure.Data;

namespace SalonCoiffure.Tests.Tests
{

    public class LoginTest
    {



        [Fact]
        public void Login_AvecInfoValide_RetourneTrue()
        {
            // Arrange
            Database.InitializeDatabase();

            string username = "admin";
            string password = "123";

            // Act
            bool result = Database.ValidateLogin(username, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Login_AvecInfoInvalide_RetourneFalse()
        {
            // Arrange
            Database.InitializeDatabase();

            string username = "admin";
            string password = "mauvaisMotDePasse";

            // Act
            bool result = Database.ValidateLogin(username, password);

            // Assert
            Assert.False(result);
        }
    }
}
