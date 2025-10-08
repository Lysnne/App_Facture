using SalonCoiffure.ViewModel;
using System.Windows.Controls;

namespace SalonCoiffure
{
    /// <summary>
    /// Logique d'interaction pour PaiementPage.xaml
    /// </summary>
    public partial class PaiementPage : Page
    {
        public PaiementPage()
        {
            InitializeComponent();
            DataContext = new PaiementViewModel();

        }
    }
}
