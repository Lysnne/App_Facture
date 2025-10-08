using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SalonCoiffure.Data;
using SalonCoiffure.ViewModel;

namespace SalonCoiffure
{
    /// <summary>
    /// Logique d'interaction pour CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        
        private CustomerViewModel _viewModel;
        public CustomerPage()
        {
            InitializeComponent();
            _viewModel = new CustomerViewModel(new CustomerDataProvider());
            DataContext = _viewModel;
            Loaded += OnLoaded;


        }
        public async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
