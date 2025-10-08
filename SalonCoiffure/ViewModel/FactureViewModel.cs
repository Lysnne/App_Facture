using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SalonCoiffure.Data;
using SalonCoiffure.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SalonCoiffure.ViewModel
{
    public partial class FactureViewModel : ObservableObject
    {

        private readonly ICustomerDataProvider _customerDataProvider;
        private readonly IServiceDataProvider _serviceDataProvider;
        private readonly IFactureDataProvider _factureDataProvider;

        public ObservableCollection<Customer> Customers { get; } = new();
        public ObservableCollection<Service> AvailableServices { get; set; }

        [ObservableProperty]
        private Customer? selectedCustomer;

        [ObservableProperty]
        private Facture? selectedFacture;
        public ObservableCollection<Facture> Factures { get; } = new();
        public ObservableCollection<Service> SelectedServices { get; } = new();

        [ObservableProperty]
        private double totalPrix;

        public FactureViewModel(ICustomerDataProvider customerProvider, IServiceDataProvider serviceProvider, IFactureDataProvider factureProvider)
        {
            _customerDataProvider = customerProvider;
            _serviceDataProvider = serviceProvider;
            _factureDataProvider = factureProvider;
            SelectedCustomer = new Customer();
            SelectedServices = new ObservableCollection<Service>();
            AvailableServices = new ObservableCollection<Service>();

        }

        public async Task LoadAsync()
        {
            Customers.Clear();
            Factures.Clear();

            var customers = await _customerDataProvider.GetAllAsync();
            var factures = await _factureDataProvider.GetAllAsync();

            if (customers != null)
                foreach (var c in customers)
                    Customers.Add(c);

            if (factures != null)
                foreach (var f in factures)
                    Factures.Add(f);


            await ReloadServices();
        }


        [RelayCommand]
        private async Task CreateFacture()
        {
            if (SelectedCustomer == null || SelectedServices.Count == 0)
                return;

            using (var context = new AppDbContext())
            {
                var facture = new Facture
                {
                    CustomerId = SelectedCustomer.Id,
                    Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0),
                    PrixTotal = SelectedServices.Sum(s => (double)s.Prix),
                    Services = new List<Service>()
                };

                foreach (var selectedService in SelectedServices)
                {
                    var existingService = context.Services.FirstOrDefault(s => s.Id == selectedService.Id);

                    if (existingService != null && !facture.Services.Contains(existingService))
                    {
                        facture.Services.Add(existingService);
                    }
                }

                context.Factures.Add(facture);
                context.SaveChanges();
            }

            SelectedServices.Clear();
            TotalPrix = 0;
            await LoadAsync();
        }






        [RelayCommand]
        private async Task DeleteFacture()
        {
            if (SelectedFacture == null)
                return;

            await _factureDataProvider.DeleteAsync(SelectedFacture.Id);
            await LoadAsync();
        }

        [RelayCommand]

        private async Task UpdateFacture()
        {
            if (SelectedFacture == null || SelectedServices.Count == 0)
                return;

            using (var context = new AppDbContext())
            {

                var factureToUpdate = context.Factures
                    .Include(f => f.Services)
                    .FirstOrDefault(f => f.Id == SelectedFacture.Id);

                if (factureToUpdate == null)
                    return;

                factureToUpdate.Services.Clear();
                foreach (var selectedService in SelectedServices)
                {
                    var serviceFromDb = context.Services.FirstOrDefault(s => s.Id == selectedService.Id);
                    if (serviceFromDb != null)
                        factureToUpdate.Services.Add(serviceFromDb);
                }

                factureToUpdate.PrixTotal = SelectedServices.Sum(s => (double)s.Prix);

                await context.SaveChangesAsync();
            }

            await LoadAsync();
        }


        private async Task ReloadServices()
        {
            AvailableServices.Clear();

            var services = await _serviceDataProvider.GetAllAsync();

            if (services != null)
            {
                foreach (var s in services)
                    AvailableServices.Add(s);
            }
        }



        public void UpdateSelectedServices(List<Service> selected)
        {
            SelectedServices.Clear();
            foreach (var service in selected)
            {
                SelectedServices.Add(service);
            }

            TotalPrix = SelectedServices.Sum(s => (double)s.Prix); // ou cast selon ton type
        }

        partial void OnSelectedFactureChanged(Facture? value)
        {
            if (value != null)
            {
                SelectedCustomer = value.Customer;
                SelectedServices.Clear();
                foreach (var s in value.Services)
                    SelectedServices.Add(s);

                TotalPrix = value.PrixTotal;

                _ = ReloadServices();
            }
        }




    }
}
