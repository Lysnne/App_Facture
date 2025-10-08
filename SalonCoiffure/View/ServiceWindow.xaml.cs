using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SalonCoiffure.Data;
using SalonCoiffure.ViewModel;

namespace SalonCoiffure
{
    /// <summary>
    /// Logique d'interaction pour ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Page
    {
        private ServiceViewModel _viewModel;

        public ServiceWindow()
        {
            InitializeComponent();
            var db = new AppDbContext();
            _viewModel = new ServiceViewModel(new ServiceDataProvider(db));
            DataContext = _viewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
