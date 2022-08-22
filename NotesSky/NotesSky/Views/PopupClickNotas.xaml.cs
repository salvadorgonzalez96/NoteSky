using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using NotesSky.Views;
using System.Collections.ObjectModel;
using NotesSky.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupClickNotas : Rg.Plugins.Popup.Pages.PopupPage
    {
        public string id { get; set; }
        public string email { get; set; }
        public string estado { get; set; }
        public PopupClickNotas(string id, string email, string estado)
        {
            InitializeComponent();
            this.id = id;
            this.email = email;
            this.estado = estado;
        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnElimina_Clicked(object sender, EventArgs e)
        {
            string r = await setestaurar(id, email, estado);
            if (r == "ACTUALIZADO")
            {
                await DisplayAlert("INFO", "Nota Eliminada", "OK");
                await Navigation.PopPopupAsync();
            }
            else
            {
                await DisplayAlert("INFO", "Nota no Eliminada", "OK");
                await Navigation.PopPopupAsync();
            }
        }
        public async Task<string> setestaurar(string nota, string email, string estados)
        {
            string estado = "";
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    string list = "";
                    if (await Controllers.AppController.setRestaurar(nota, email, estados) != null)
                    {
                        list = await Controllers.AppController.setRestaurar(nota, email, estados);
                        if (list == "ACTUALIZADO")
                        {
                            estado = "ACTUALIZADO";
                        }
                        else
                        {
                            estado = "No ACTUALIZADO";
                        }
                    }
                    else
                    {
                        estado = "No ACTUALIZADO";
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                estado = "INCORRECTO";
            }
            return estado;
        }
        private async void btnFavorite_Clicked(object sender, EventArgs e)
        {
            string r = await setestaurar2(id, email, "1");
            if (r == "ACTUALIZADO")
            {
                await DisplayAlert("INFO", "Nota Agregada a Favoritos", "OK");
                await Navigation.PopPopupAsync();
            }
            else
            {
                await DisplayAlert("INFO", "Nota Agregada", "OK");
                await Navigation.PopPopupAsync();
            }
        }
        public async Task<string> setestaurar2(string nota, string email, string estados)
        {
            string estado = "";
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    string list = "";
                    if (await Controllers.AppController.setRestaurarFav(nota, email, estados) != null)
                    {
                        list = await Controllers.AppController.setRestaurarFav(nota, email, estados);
                        if (list == "ACTUALIZADO")
                        {
                            estado = "ACTUALIZADO";
                        }
                        else
                        {
                            estado = "No ACTUALIZADO";
                        }
                    }
                    else
                    {
                        estado = "No ACTUALIZADO";
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                estado = "INCORRECTO";
            }
            return estado;
        }
        private async void btnAbrir_Clicked(object sender, EventArgs e)
        {
            //Style styles = new Style(typeof(NavigationPage));
            //styles.Setters.Add(new Setter(NavigationPage.BarBackgroundColorProperty, OSAppTheme.Dark));


            //var entryStyle = new Style(typeof(NavigationPage))
            //{
            //    Setters = {
            //    new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = AppThemeBindingExtension }
            //}
            //};
            NavigationPage nv = new NavigationPage(new RecordatoriosVistaPrevia(id,email));
            nv.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, Color.White, Color.FromHex("#2E312E"));
            //nv.SetAppThemeColor(NavigationPage.BarTextColorProperty, Color.White, Color.FromHex("#2E312E"));
            //nv.SetAppThemeColor(NavigationPage.IconColorProperty, Color.White, Color.FromHex("#2E312E"));
            //nv.SetAppThemeColor(NavigationPage.BackgroundProperty, Color.White, Color.FromHex("#2E312E"));
            await Navigation.PushModalAsync(nv);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}