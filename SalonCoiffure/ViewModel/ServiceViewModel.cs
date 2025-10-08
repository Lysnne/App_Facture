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
    public partial class ServiceViewModel : ObservableObject
    {
        private readonly IServiceDataProvider _serviceDataProvider;

        [ObservableProperty]
        private Service selectedService;

        [ObservableProperty]
        private Service newService = new Service();


        [ObservableProperty]
        private string searchText;

        public ObservableCollection<Service> Services { get; } = new ObservableCollection<Service>();
        public ObservableCollection<Service> FilteredServices { get; } = new ObservableCollection<Service>();

        public ServiceViewModel(IServiceDataProvider serviceDataProvider)
        {
            _serviceDataProvider = serviceDataProvider;
            SelectedService = new Service();
        }

        public async Task LoadAsync()
        {
            var services = await _serviceDataProvider.GetAllAsync();
            if (services != null)
            {
                Services.Clear();
                FilteredServices.Clear();
                foreach (var service in services)
                {
                    Services.Add(service);
                    FilteredServices.Add(service);
                }
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterServices();
        }

        private void FilterServices()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredServices.Clear();
                foreach (var service in Services)
                {
                    FilteredServices.Add(service);
                }
            }
            else
            {
                var filtered = Services
                    .Where(s => s.Nom.Contains(SearchText, System.StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

                FilteredServices.Clear();
                foreach (var service in filtered)
                {
                    FilteredServices.Add(service);
                }
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            if (string.IsNullOrWhiteSpace(SelectedService?.Nom))
            {
                MessageBox.Show("Le champ Nom est requis.");
                return;
            }

            var newService = new Service
            {
                Nom = SelectedService.Nom,
                Prix = SelectedService.Prix
            };

            await _serviceDataProvider.AddAsync(newService);
            Services.Add(newService);
            FilteredServices.Add(newService);
            SelectedService = new Service(); 
        }


        [RelayCommand]
        private async Task Update()
        {
            if (SelectedService == null)
            {
                MessageBox.Show("Veuillez sélectionner un service à modifier.");
                return;
            }

            try
            {
                await _serviceDataProvider.UpdateAsync(SelectedService);
                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour :");
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (SelectedService == null)
            {
                MessageBox.Show("Veuillez sélectionner un service à supprimer.");
                return;
            }



            try
            {
                await _serviceDataProvider.DeleteAsync(SelectedService);
                await LoadAsync();
                SelectedService = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression");
            }
        }

    }
}
