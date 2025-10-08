using SalonCoiffure.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Navigation;

namespace SalonCoiffure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private User? _currentUser;
        public User? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserFullName));
            }
        }

        public string UserFullName => CurrentUser != null ? $"{CurrentUser.Prenom} {CurrentUser.Nom}" : "";

  
        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomerPage());
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServiceWindow());
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void PaiementButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PaiementPage());
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ContactPage());

        }

        private void FactureButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FacturePage());

        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage(this));

        }        
        
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = null;
            MainFrame.Navigate(new LoginPage(this));

        }



    }
}