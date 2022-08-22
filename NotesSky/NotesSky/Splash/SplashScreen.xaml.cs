using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Splash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        Boolean btn1=false;
        Boolean btn2=false;
        Boolean btn3=false;
        public SplashScreen()
        {
            InitializeComponent();
            //GifdeInicio();
            Animation();
            VerificaInicio();
        }

        private async void GifdeInicio()
        {
            await Navigation.PushModalAsync(new GifPageSplash());
        }

        private async void VerificaInicio()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string email = "";
                string pass = "";
                string nom = "";
                string ids = "";
                string passDecript = "";
                string estaLogeado = "False";
                try
                {
                    email = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Email");
                }
                catch
                {
                    email = "";
                }
                try
                {
                    pass = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Pass");
                    passDecript = Config.Ecript.Decrypt(pass);
                }
                catch
                {
                    pass = "";
                    passDecript = "";
                }
                try
                {
                    nom = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Nom");
                }
                catch
                {
                    nom = "";
                }
                try
                {
                    ids = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Ids");
                }
                catch
                {
                    ids = "";
                }
                try
                {
                    estaLogeado = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Login");
                }
                catch
                {
                    estaLogeado = "";
                }
                //y no tiene datos persistentes
                //hira al login
                if (!string.IsNullOrEmpty(estaLogeado))//si la variable persistente existe
                {
                    if (estaLogeado == "true")//si esta varaible contiene true esta logeado aun
                    {
                        await Navigation.PushModalAsync(new Views.PaginaSwipe(email, passDecript, nom, ids));
                    }
                    else //sino es porque cerro sesion
                    {
                        await Navigation.PushModalAsync(new Views.PantallaLogin());
                    }
                }
                else 
                {
                    if (string.IsNullOrEmpty(passDecript) || string.IsNullOrEmpty(email))
                    {
                        //await Navigation.PushModalAsync(new Views.PantallaLogin());
                    }
                    else if (string.IsNullOrEmpty(passDecript) && string.IsNullOrEmpty(email))
                    {
                        //await Navigation.PushModalAsync(new Views.PantallaLogin());
                    }
                    else //sino ninguna de las 3 variables contiene algo siempre se va al login
                    {

                    }
                }
            }
            else
            {
                //salir de la app
                await DisplayAlert("ALERTA","NO ESTAS CONECTADO A INTERNET","OK");
            }
        }

        public async Task Animation() 
        {
            img1.Opacity = 0;
            await img1.FadeTo(1, 4000);
            await gridBtn.FadeTo(1, 4000,Easing.SinIn);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!btn1&!btn2&!btn3) 
            {
                img1.Source = null;
                img1.Source = "reminders.png";
                txtstatus.Text = "Agenda Recordatorios Rapido!";
                Animation();
                imgbtn1.BackgroundColor = Color.FromHex("#B7C0F5");
                imgbtn3.BackgroundColor = Color.FromHex("#B7C0F5");
                imgbtn2.BackgroundColor = Color.FromHex("#6072EC");
                btn1 = true;
            }
            else if (btn1 & !btn2 & !btn3)
            {
                img1.Source = null;
                img1.Source = "seguridad.png";
                txtstatus.Text = "Notas 100% Seguras!";
                Animation();
                imgbtn1.BackgroundColor = Color.FromHex("#B7C0F5");
                imgbtn2.BackgroundColor = Color.FromHex("#B7C0F5");
                imgbtn3.BackgroundColor = Color.FromHex("#6072EC");
                btn2 = true;
            }
            else if(btn1 & btn2 & !btn3) 
            {
                //si tiene acceso a internet y
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string email = "";
                    string pass = "";
                    string nom = "";
                    string ids = "";
                    string passDecript = "";
                    string estaLogeado = "False";
                    try 
                    {
                        email = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Email");
                    }
                    catch 
                    {
                        email = "";
                    }
                    try
                    {
                        nom = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Nom");
                    }
                    catch
                    {
                        nom = "";
                    }
                    try
                    {
                        ids = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Ids");
                    }
                    catch
                    {
                        ids = "";
                    }
                    try
                    {
                        pass = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Pass");
                        passDecript = Config.Ecript.Decrypt(pass);
                    }
                    catch
                    {
                        pass = "";
                        passDecript = "";
                    }
                    try
                    {
                        estaLogeado = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Login");
                    }
                    catch
                    {
                        estaLogeado = "";
                    }
                    //y no tiene datos persistentes
                    //hira al login
                    if (string.IsNullOrEmpty(passDecript) || string.IsNullOrEmpty(email))
                    {
                        await Navigation.PushModalAsync(new Views.PantallaLogin());
                    }
                    else if (string.IsNullOrEmpty(passDecript) && string.IsNullOrEmpty(email))
                    {
                        await Navigation.PushModalAsync(new Views.PantallaLogin());
                    }
                    else if(!string.IsNullOrEmpty(estaLogeado))//si la variable persistente existe
                    {
                        if (estaLogeado == "true")//si esta varaible contiene true esta logeado aun
                        {
                            await Navigation.PushModalAsync(new Views.PaginaSwipe(email, passDecript, nom, ids));
                        }
                        else //sino es porque cerro sesion
                        {
                            await Navigation.PushModalAsync(new Views.PantallaLogin());
                        }
                    }
                    else //sino ninguna de las 3 variables contiene algo siempre se va al login
                    {
                        await Navigation.PushModalAsync(new Views.PantallaLogin());
                    }

                }
                else 
                {
                    //salir de la app
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}