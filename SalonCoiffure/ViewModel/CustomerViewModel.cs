using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalonCoiffure.Data;
using SalonCoiffure.Model;

namespace SalonCoiffure.ViewModel
{
    public partial class CustomerViewModel : ObservableObject
    {

        private readonly ICustomerDataProvider _customerDataProvider;

        [ObservableProperty]
        private Customer _selectedCustomer;
        
        [ObservableProperty]
        private Customer newCustomer = new Customer();

        [ObservableProperty]
        private string searchText;

        public ObservableCollection<Customer> Customers { get; } = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> FilteredCustomers { get; } = new ObservableCollection<Customer>();

        public CustomerViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
            SelectedCustomer = new Customer();
        }

        public async Task LoadAsync()
        {
            var customers = await _customerDataProvider.GetAllAsync();
            

            if (customers != null)
            {
                Customers.Clear();
                FilteredCustomers.Clear();

                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                    FilteredCustomers.Add(customer);
                }
            }
        }
        partial void OnSearchTextChanged(string value)
        {
            FilterCustomers();
        }

        private void FilterCustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                //afficher tout
                FilteredCustomers.Clear();
                foreach (var customer in Customers)
                {
                    FilteredCustomers.Add(customer);
                }
            }
            else
            {
                var filtered = Customers
                    .Where(c => c.Nom.Contains(SearchText))
                    .ToList();

                FilteredCustomers.Clear();
                foreach (var customer in filtered)
                {
                    FilteredCustomers.Add(customer);
                }
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Le champ Nom est requis.");
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedCustomer.Nom))
            {
                
                MessageBox.Show("Le nom est obligatoire.", "Erreur");
                return;
            }

            var newCustomer = new Customer
            {
                Nom = SelectedCustomer.Nom,
                Telephone = SelectedCustomer.Telephone,
                Email = SelectedCustomer.Email,
                Adresse = SelectedCustomer.Adresse
            };

            await _customerDataProvider.AddAsync(newCustomer);

            Customers.Add(newCustomer);
            FilteredCustomers.Add(newCustomer);

            SelectedCustomer = new Customer();
        }

        [RelayCommand]
        private async Task Update()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier.");
                return;
            }

            try
            {
                await _customerDataProvider.UpdateAsync(SelectedCustomer);
                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour" );
            }
        }


        [RelayCommand]
        private async Task Delete()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.");
                return;
            }

        

            try
            {
                await _customerDataProvider.DeleteAsync(SelectedCustomer);
                await LoadAsync();
                SelectedCustomer = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression");
            }
        }

    }
}
