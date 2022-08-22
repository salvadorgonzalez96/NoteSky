using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using NotesSky.Models;
using System.Windows.Input;
using System.Threading.Tasks;

namespace NotesSky.Configuraciones.View
{
  public  class ViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { private set; get; }
        public ViewModel()
        {
            var theme = Preferences.Get("OSAppTheme", Enum.GetName(typeof(OSAppTheme), Xamarin.Forms.OSAppTheme.Unspecified));
            OSAppTheme = (int)Enum.Parse(typeof(OSAppTheme), theme);
            RefreshCommand = new Command(async () => await LoadPublications());
        }
        private ObservableCollection<Notas> _listaNotas;
        public ObservableCollection<string> Themes => new ObservableCollection<string> { "Predeterminado", "Claro", "Oscuro" };
        async Task LoadPublications()
        {
            // code omitted

            IsRefreshing = false;
        }
        int osAppTheme;
        public int OSAppTheme
        {
            get => osAppTheme;
            set
            {
                if (osAppTheme == value)
                {
                    return;
                }

                Preferences.Set("OSAppTheme", Enum.GetName(typeof(OSAppTheme), value));
                App.Current.UserAppTheme = (OSAppTheme)value;
                SetProperty(ref osAppTheme, value);
            }

        }
    }
}
