using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NotesSky.Models;
using NotesSky.Views;
using NotesSky.Views.PantallasCambiocontra;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantallaLogin : ContentPage
    {
        public PantallaLogin()
        {
            InitializeComponent();
            
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string email = "";
            string pass = "";
            string passDecript = "";
            string estaLogeado = "False";
            //SI VENGO DE EL MAIN Y YA INICIE POR PRIMERA VEZ Y TENGO CUENTA INICIADA
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
                
            }
            else if (string.IsNullOrEmpty(passDecript) && string.IsNullOrEmpty(email))
            {
                
            }
            else if (!string.IsNullOrEmpty(estaLogeado))//si la variable persistente existe
            {
                if (estaLogeado == "true")//si esta varaible contiene true esta logeado aun
                {
                    contra vu = new contra();
                    try
                    {
                        vu = await Controllers.AppController.getLogin(email);
                        if (!String.IsNullOrEmpty(vu.nombre))
                        {
                            await Navigation.PushModalAsync(new Views.PaginaSwipe(email, passDecript, vu.nombre, vu.id));
                        }
                        else
                        {
                            await DisplayAlert("ERROR", "OCURRIO UN ERROR Vuelve a Intentarlo!", "OK");
                        }
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                    }
                }
                else //sino es porque cerro sesion
                {
                    
                }
            }
            else //sino ninguna de las 3 variables contiene algo siempre se va al login
            {
                
            }

        }
        //Boton para iniciar Sesion
        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new);
            //ENVIAR AL PERSISTENT DATA EL CORREO Y CONTRASEÑA
            await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Email", txtCorreo.Text);
            await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Pass", Config.Ecript.Encrypt(txtCorreo.Text));
        }
        //Boton para crear usuario
        private async void btnCrear_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CrearUsuario());
        }
       //Boton que olvide la clave
        private async void btnclave_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new Codigocontra(txtCorreo.Text));
        }
        //cambio de estado del checkbox
        private void mostrar_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var estavisible = txtContra.IsPassword;
            if (estavisible) 
            {
                txtContra.IsPassword = false;
            }
            else 
            {
                txtContra.IsPassword = true;
            }
        }

        private async void btnInicia_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                contra vu = new contra();
                bool toodbien = false;
                if (!string.IsNullOrEmpty(txtCorreo.Text))
                {
                    if (!string.IsNullOrEmpty(txtContra.Text))
                    {
                        try
                        {
                            vu = await Controllers.AppController.getLogin(txtCorreo.Text);
                            if (String.IsNullOrEmpty(vu.nombre)) { toodbien = false; }
                            else { toodbien = true; }
                        }
                        catch(System.Net.Sockets.SocketException ex)
                        {
                            await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                            Console.WriteLine(ex);
                        }
                    }
                    else
                    {
                        await DisplayAlert("INFO", "DEBE INGRESAR SU CONTRASEÑA!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("INFO", "DEBE INGRESAR EL CORREO!", "OK");
                }
                if (toodbien)
                {
                    if (string.IsNullOrEmpty(vu.codigo) && string.IsNullOrEmpty(vu.pass) && string.IsNullOrEmpty(vu.tipo))
                    {
                        await DisplayAlert("ALERTA", "USUARIO NO EXISTE!", "OK");
                    }
                    else if (string.IsNullOrEmpty(vu.codigo))
                    {
                        if (!string.IsNullOrEmpty(vu.pass))
                        {
                            if (!string.IsNullOrEmpty(vu.nombre))
                            {
                                //AHORA VALIDAR QUE EL CORREO Y CONTRASEÑA SEAN VALIDOS Con la DB
                                if (Config.Ecript.Decrypt(vu.pass) == txtContra.Text)
                                {
                                    bool persistenciafallo = false;
                                    try
                                    {
                                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Email", txtCorreo.Text);
                                    }
                                    catch
                                    {
                                        persistenciafallo = true;
                                    }
                                    try
                                    {
                                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Nom", vu.nombre);
                                    }
                                    catch
                                    {
                                        persistenciafallo = true;
                                    }
                                    try
                                    {
                                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Ids", vu.id);
                                    }
                                    catch
                                    {
                                        persistenciafallo = true;
                                    }
                                    try
                                    {
                                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Pass", Config.Ecript.Encrypt(txtCorreo.Text));
                                    }
                                    catch
                                    {
                                        persistenciafallo = true;
                                    }
                                    try
                                    {
                                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Login", "true");
                                    }
                                    catch
                                    {
                                        persistenciafallo = true;
                                    }
                                    if (!persistenciafallo)
                                    {
                                        txtCorreo.Text = "";
                                        txtContra.Text = "";
                                        await Navigation.PushModalAsync(new Views.PaginaSwipe(txtCorreo.Text, Config.Ecript.Encrypt(txtCorreo.Text), vu.nombre, vu.id));
                                    }
                                    else
                                    {
                                        await DisplayAlert("ERROR", "PORFAVOR. Intentelo de nuevo!. \nOcurrio un Error de nuestro lado", "OK");
                                    }
                                    //await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Email", txtCorreo.Text);
                                    //await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Pass", Config.Ecript.Encrypt(txtCorreo.Text));
                                }
                                else
                                {
                                    await DisplayAlert("Contraseña INCORRECTA", "PORFAVOR. Intentelo de nuevo!", "OK");
                                }
                            }
                        }
                        else
                        {
                            await DisplayAlert("INFO", "PORFAVOR. Intentelo de nuevo!", "OK");
                        }
                    }
                    else
                    {
                        //EL USUARIO NO AH VERIFICADO SU CUENTA
                        await DisplayAlert("INFO", "POR FAVOR. REVISE SU CORREO, Y VERIFIQUE SU CODIGO!", "OK");
                        await Navigation.PushModalAsync(new Verificacion(txtCorreo.Text));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(vu.codigo) && string.IsNullOrEmpty(vu.pass) && string.IsNullOrEmpty(vu.tipo))
                    {
                        await DisplayAlert("ALERTA", "USUARIO NO EXISTE!", "OK");
                    }
                    else if (string.IsNullOrEmpty(vu.codigo))
                    {
                        if (!string.IsNullOrEmpty(vu.pass))
                        {
                            if (!string.IsNullOrEmpty(vu.nombre))
                            {
                                await DisplayAlert("INFO", "PORFAVOR. Intentelo de nuevo!", "OK");
                            }
                        }
                        else { await DisplayAlert("Contraseña INCORRECTA", "PORFAVOR. Intentelo de nuevo!", "OK"); }
                    }
                    else { await DisplayAlert("INFO", "PORFAVOR. REVISE SU CORREO, Y VERIFIQUE SU CUENTA!", "OK"); }
                }
            }
            else 
            {
                await DisplayAlert("ALERTA", "Conectate a La red. Para Iniciar Sesion!", "OK");
            }
        }

        private async void btnCrea_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await Navigation.PushModalAsync(new CrearUsuario());
            }
        }

        private void clickOjo_Tapped(object sender, EventArgs e)
        {
            var estavisible = txtContra.IsPassword;
            if (!estavisible)
            {
                txtContra.IsPassword = true;
                imgeye.Source = "closeeye.png";
                
            }
            else
            {
                txtContra.IsPassword = false;
                imgeye.Source = "openeye.png";
            }
        }
    }
}