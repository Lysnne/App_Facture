using CommunityToolkit.Mvvm.Input;
using SalonCoiffure.Data;
using SalonCoiffure.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace SalonCoiffure.ViewModel
{
    public class PaiementViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Facture> Factures { get; set; }

        public ICommand SelectModePaiementCommand { get; set; }
        public ICommand EnregistrerPaiementCommand { get; set; }

        private Facture _selectedFacture;
        public Facture SelectedFacture
        {
            get => _selectedFacture;
            set
            {
                _selectedFacture = value;
                OnPropertyChanged();
                Montant = _selectedFacture?.PrixTotal ?? 0;
                Message = "";
            }
        }

        private double _montant;
        public double Montant
        {
            get => _montant;
            set
            {
                _montant = value;
                OnPropertyChanged();
            }
        }

        

        private string _moyenPaiement;
        public string MoyenPaiement
        {
            get => _moyenPaiement;
            set
            {
                _moyenPaiement = value;
                OnPropertyChanged();
                UpdateButtonBackgrounds();
            }
        }

        public Brush CarteButtonBackground { get; set; } = Brushes.LightGray;
        public Brush VirementButtonBackground { get; set; } = Brushes.LightGray;
        public Brush GoogleAppleButtonBackground { get; set; } = Brushes.LightGray;

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public PaiementViewModel()
        {
            
            using (var db = new AppDbContext())
            {
                var facturesSansPaiement = db.Factures
                    .Where(f => f.Paiement == null)
                    .ToList();

                Factures = new ObservableCollection<Facture>(facturesSansPaiement);
            }

            
            SelectModePaiementCommand = new RelayCommand<object>(param =>
            {
                if (param != null)
                {
                    MoyenPaiement = param.ToString();
                }
            });

            EnregistrerPaiementCommand = new RelayCommand(() =>
            {
                if (SelectedFacture == null || string.IsNullOrEmpty(MoyenPaiement))
                {
                    Message = "Veuillez sélectionner une facture et un mode de paiement.";
                    return;
                }

                using (var db = new AppDbContext())
                {
                    var facture = db.Factures.FirstOrDefault(f => f.Id == SelectedFacture.Id);
                    if (facture != null)
                    {
                        var paiement = new Paiement
                        {
                            FactureId = facture.Id,
                            Montant = facture.PrixTotal,
                            DatePaiement = DateTime.Now,
                            MoyenPaiement = MoyenPaiement
                        };

                        db.Paiements.Add(paiement);
                        db.SaveChanges();

                        
                        Factures.Remove(SelectedFacture);
                        SelectedFacture = null;
                        MoyenPaiement = null;
                        Message = "Paiement enregistré avec succès.";
                    }
                    else
                    {
                        Message = "Erreur : facture introuvable.";
                    }
                }
            });
        }

        private void UpdateButtonBackgrounds()
        {
            CarteButtonBackground = MoyenPaiement == "Carte" ? Brushes.LightGreen : Brushes.LightGray;
            VirementButtonBackground = MoyenPaiement == "Virement" ? Brushes.LightGreen : Brushes.LightGray;
            GoogleAppleButtonBackground = MoyenPaiement == "Google/Apple Pay" ? Brushes.LightGreen : Brushes.LightGray;

            OnPropertyChanged(nameof(CarteButtonBackground));
            OnPropertyChanged(nameof(VirementButtonBackground));
            OnPropertyChanged(nameof(GoogleAppleButtonBackground));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
