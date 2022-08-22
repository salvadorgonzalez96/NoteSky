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
    public partial class PopupClickPapelera : Rg.Plugins.Popup.Pages.PopupPage
    {
        private string v;

        public string id { get; set; }
        public string email { get; set; }
        public string estado { get; set; }
        public Action metodo { get; set; }
        public PopupClickPapelera(string id, string email)
        {
            InitializeComponent();
            this.id = id;
            this.email = email;
        }

        public PopupClickPapelera(string id, string email, string estado) 
        {
            InitializeComponent();
            this.id = id;
            this.email = email;
            this.estado=estado;
            this.metodo = metodo;
        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            
        }
        private async void btnRestaura_Clicked(object sender, EventArgs e)
        {
            string r = await setestaurar(id,email, estado);
            if(r== "ACTUALIZADO")
            {
                await DisplayAlert("INFO","Nota Restaurada","OK");
                await Navigation.PopPopupAsync();
            }
            else
            {
                await DisplayAlert("INFO", "Nota no Restaurada", "OK");
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
        private async void btnOpen_Clicked(object sender, EventArgs e)
        {
            NavigationPage nv = new NavigationPage(new RecordatoriosVistaPrevia(id, email));
            nv.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, Color.White, Color.FromHex("#2E312E"));
            await Navigation.PushModalAsync(nv);
            await PopupNavigation.Instance.PopAsync();
        }

        private void PopupPage_BackgroundClicked_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}