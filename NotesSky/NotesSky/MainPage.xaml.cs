using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NotesSky
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnvista_Clicked(object sender, EventArgs e)
        {
            //RecordatoriosVistaPrevia vp = new RecordatoriosVistaPrevia("0");
            //await Navigation.PushAsync(new RecordatoriosVistaPrevia("0"));
        }

        private async void btnedit_Clicked(object sender, EventArgs e)
        {
            Object nota = new
            {
                id = 1,
                titulo = "NOTA1",
                contenido = "HOLA COMO ESTAS AMIGO AKI MOSTRANDO LA NOTA NUMERO 1",
                tengoAudio = false,
                tengoFoto = false
            };
            RecordatoriosEditar pag = new RecordatoriosEditar();
            pag.BindingContext = nota;
            await Navigation.PushAsync(pag);
        }

        private async void btnmenu_Clicked(object sender, EventArgs e)
        {
            //Salvador.FlyoutPage1 flyy = new Salvador.FlyoutPage1();
            //flyy.Detail.BackgroundColor = Color.White;
            //flyy.Detail.Title = "holaa";
            //flyy.Flyout.BackgroundColor = Color.White;
            //await Navigation.PushModalAsync(flyy);
        }

        private async void btnmenu2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.PaginaSwipe("", "", "", ""));
        }

        private async void btnpage1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Page1());
        }

        private async void btnpage3_Clicked(object sender, EventArgs e)
        {
            //AKI VA ANTES UN VALIDADOR QUE SI YA EJECUTO POR PRIMERA VEZ NO VOLVER A MOSTRAR EL SPLASHSCREEN

            bool algofallo = false;
            string existe = "";

            try
            {
                existe = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_FirtApp");
            }
            catch
            {
                existe = "";
            }
            //SI ya existe esa variable no abrir el splashscreen sino el login
            if (existe == "true")
            {
                await Navigation.PushModalAsync(new Views.PantallaLogin());
            }
            else
            {
                try
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_FirtApp", "true");
                }
                catch
                {
                    algofallo = true;
                }
                if (!algofallo)
                {
                    await Navigation.PushModalAsync(new Splash.SplashScreen());
                }
                else
                {
                    await DisplayAlert("ERROR", "ALGO FALLO. Intenta volver a abrir la app", "OK");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
        }

        private async void btnpage4_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.PantallaLogin());
        }
    }
}
