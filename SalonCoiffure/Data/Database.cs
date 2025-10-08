using SalonCoiffure.Model;
using System.Windows;

namespace SalonCoiffure.Data
{
    public static class Database
    {
        public static void InitializeDatabase()
        {
            try
            {
                using var context = new AppDbContext();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Username = "admin", Password = "123", Prenom = "Terrieur", Nom = "Alex", Email = "alex123@col.com", Telephone = "5145556325" },
                        new User { Username = "emp", Password = "emp123", Prenom = "John", Nom = "Doe", Email = "john123@col.com", Telephone = "5145556325" }



                    );
                    context.SaveChanges();
                }
                if (!context.Services.Any())
                {
                    context.Services.AddRange(
                          new Service { Nom = "Coupe cheveux", Prix = 25.00m },
                          new Service { Nom = "Coloration", Prix = 50.00m },
                          new Service { Nom = "Soin capillaire", Prix = 40.00m }
                        );
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur DB : " + ex.Message);
            }

        }

        public static bool ValidateLogin(string username, string password)
        {
            using var context = new AppDbContext();

            var user = context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            return user != null;
        }


    }
}


