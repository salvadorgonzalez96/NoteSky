using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NotesSky.Views;
using NotesSky.Views.PantallasCambiocontra;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views.PantallasCambiocontra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Codigocontra : ContentPage
    {
        int seg = 60;
        bool esperando;
        public string email { get; set; }
        public Codigocontra(string email)
        {
            InitializeComponent();
            this.email = email;
            txtCorreo.Text = email;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (String.IsNullOrEmpty(email)) { Frame1.IsEnabled = false; Frame2.IsEnabled = false; }
            else { Frame1.IsEnabled = true; Frame2.IsEnabled = true; }
        }
        //private async void btncambiarclave_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Cambiarcontra());
        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        //private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new Cambiarcontra());
        //}

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }
        public int getRandomInteger(int minimum, int maximum)
        {
            return new Random().Next(minimum, maximum);
        }
        private async void btnenviarCodCambioPass_Tapped(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                int rand = getRandomInteger(1000, 10000);
                string estado = "";
                string respuesta = "";
                esperando = true;
                if (!String.IsNullOrEmpty(txtCorreo.Text))
                {
                    using (UserDialogs.Instance.Loading("ESPERE..."))
                    {
                        respuesta = await Controllers.AppController.setNewCodClave(txtCorreo.Text, "" + rand);
                    }
                    UserDialogs.Instance.HideLoading();
                    if (respuesta == "NO ACTUALIZADO")
                    {
                        await DisplayAlert("ALERTA", "Fallo AL ENVIAR SU CODIGO, Vuelvelo a intentar", "OK");
                    }
                    else if (respuesta == "ACTUALIZADO")
                    {
                        try
                        {
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                            mail.From = new MailAddress("noteskyhn@gmail.com");
                            mail.To.Add(txtCorreo.Text);
                            mail.Subject = "CODIGO DE VALIDACION DE CUENTA";
                            mail.Body = "CODIGO:" + rand;

                            SmtpServer.Port = 587;
                            SmtpServer.Host = "smtp.gmail.com";
                            SmtpServer.EnableSsl = true;
                            SmtpServer.UseDefaultCredentials = false;
                            SmtpServer.Credentials = new System.Net.NetworkCredential("noteskyhn@gmail.com", "epsmsbbeohnaalba");

                            SmtpServer.Send(mail);
                            estado = "CORREO ENVIADO";
                        }
                        catch (Exception ex)
                        {
                            estado = "MAIL fallido";
                        }
                        if (estado.Contains("CORREO ENVIADO"))
                        {
                            await DisplayAlert("INFO", "CODIGO ENVIADO A SU CORREO", "OK");
                            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                            {
                                seg--;
                                if (seg > 0)
                                {
                                    esperando = true;
                                    Frame1.IsEnabled = false;
                                    btnVolverEnviar.Text = "ESPERE..." + seg.ToString();
                                }
                                if (seg == 0)
                                {
                                //detener tiempo
                                    esperando = false;
                                    Frame1.IsEnabled = true;
                                    btnVolverEnviar.Text = "Enviar Codigo";
                                    seg = 60;
                                }
                                return esperando;
                            });
                        }
                        else if (estado.Contains("MAIL fallido"))
                        {
                            await DisplayAlert("ALERTA", "CODIGO NO ENVIADO!", "OK");
                        }
                    }
                    else if (respuesta == "INGRESE DATOS PRIMERO!")
                    {
                        await DisplayAlert("ALERTA", "FALTAN DATOS PARA ENVIAR CODIGO", "OK");
                    }
                    else if (respuesta == "USUARIO NO EXISTE o NO VERIFICADO")
                    {
                        await DisplayAlert("ALERTA", "USUARIO NO EXISTE o NO HA SIDO VERIFICADO", "OK");
                    }
                    else if (respuesta == "LA PAGINA NO RESPONDIO")
                    {
                        await DisplayAlert("ALERTA", "PAGINA NO RESPONDE", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("ALERTA", "DEBES INGRESAR EL CORREO", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "DEBES CONECTARTE A LA RED", "OK");
            }
        }

        private async void btnClaveCambioPass_Tapped(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (!String.IsNullOrEmpty(txtcodigo.Text))
                {
                    string respuesta = "";
                    using (UserDialogs.Instance.Loading("ESPERE..."))
                    {
                        respuesta = await Controllers.AppController.setConfirmCodChangePass(txtCorreo.Text, txtcodigo.Text);
                    }
                    UserDialogs.Instance.HideLoading();
                    if (respuesta == "NO CONFIRMADO")
                    {
                        await DisplayAlert("ALERTA", "CODIGO NO VALIDO, INTENTELO DE NUEVO", "OK");
                    }
                    else if (respuesta == "CONFIRMADO")
                    {
                        await DisplayAlert("INFO", "CODIGO CONFIRMADO,\nINGRESE SU CLAVE EN LA SIG VENTANA", "OK");
                        await Navigation.PushModalAsync(new Cambiarcontra(txtCorreo.Text));
                    }
                    else if (respuesta == "INGRESE DATOS PRIMERO!" || respuesta == "LA PAGINA NO RESPONDIO")
                    {
                        await DisplayAlert("ALERTA", "FALTARON DATOS O LA PAGINA NO RESPONDIO", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "DEBES INGRESAR EL CODIGO PRIMERO", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "DEBES CONECTARTE A LA RED", "OK");
            }
        }

        private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCorreo.Text))
            {
                Frame1.IsEnabled = true;
                Frame2.IsEnabled = true;
            }
            else
            {
                Frame1.IsEnabled = false;
                Frame2.IsEnabled = false;
            }
        }
    }
}