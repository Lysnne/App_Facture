using SalonCoiffure.Data;
using SalonCoiffure.Model;
using SalonCoiffure.ViewModel;
using QuestPDF;
using QuestPDF.Helpers;
using QuestPDF.Fluent;
using System.Windows;
using System.Windows.Controls;
using QuestPDF.Infrastructure;

namespace SalonCoiffure
{
    /// <summary>
    /// Logique d'interaction pour FacturePage.xaml
    /// </summary>
    public partial class FacturePage : Page
    {
        
        private readonly FactureViewModel _viewModel;

        public FacturePage()
        {
            InitializeComponent();

            var db = new AppDbContext();

            
            var customerProvider = new CustomerDataProvider(); 
            var serviceProvider = new ServiceDataProvider(db);
            var factureProvider = new FactureDataProvider(db); 

            _viewModel = new FactureViewModel(customerProvider, serviceProvider, factureProvider);
            DataContext = _viewModel;

            Loaded += ReceiptWindow_Loaded;
        }

        private async void ReceiptWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void ServicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedServices = ServicesListBox.SelectedItems.Cast<Service>().ToList();
            _viewModel.UpdateSelectedServices(selectedServices);
        }

        private void PdfGenerator_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "FacturesDashStack",
                DefaultExt = ".pdf",
                Filter = "Document PDF (.pdf)|*.pdf"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string path = dlg.FileName;

                QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(QuestPDF.Helpers.Colors.White);

                        page.Header()
                            .Text("Factures")
                            .Bold().FontSize(50).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(2);
                                    });

                                    void AddRow(string label, string value)
                                    {
                                        table.Cell().Element(CellStyle).Text(label).SemiBold();
                                        table.Cell().Element(CellStyle).Text(value);
                                    }

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .PaddingVertical(5)
                                            .PaddingHorizontal(10)
                                            .BorderBottom(1)
                                            .BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2);
                                    }

                                    AddRow("Client", ClientName.Text);
                                    var selectedServices = this.DataContext as FactureViewModel;
                                    if (selectedServices != null)
                                    {
                                        foreach (var service in selectedServices.SelectedServices)
                                        {
                                            AddRow("Service", service.Nom);
                                        }
                                    }
                                    AddRow("Prix Total ($)", TotalPrice.Text);
                                });
                            });

                    });
                }).GeneratePdf(path);

                MessageBox.Show("PDF généré avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void EmailFormOpener_Click(object sender, EventArgs e)
        {
            EmailForm emailForm = new EmailForm();
            emailForm.Show();
        }
    }
}
