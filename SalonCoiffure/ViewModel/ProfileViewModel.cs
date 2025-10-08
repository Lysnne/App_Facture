using SalonCoiffure.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SalonCoiffure.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Nom));
                OnPropertyChanged(nameof(Prenom));
                OnPropertyChanged(nameof(Telephone));
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Nom => User?.Nom;
        public string Prenom => User?.Prenom;
        public string Telephone => User?.Telephone;
        public string Email => User?.Email;

        public ProfileViewModel(User user)
        {
            User = user;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

