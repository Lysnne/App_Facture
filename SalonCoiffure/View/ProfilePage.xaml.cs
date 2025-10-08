using SalonCoiffure.Model;
using SalonCoiffure.ViewModel;
using System.Windows.Controls;

namespace SalonCoiffure
{
    /// <summary>
    /// Logique d'interaction pour ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {


        public ProfilePage()
        {
            InitializeComponent();
            
        }
        public ProfilePage(User user)
        {
            InitializeComponent();
            DataContext = new ProfileViewModel(user);
        }

    }
}
