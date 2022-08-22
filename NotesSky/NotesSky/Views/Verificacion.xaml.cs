using Acr.UserDialogs;
using NotesSky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Verificacion : ContentPage
    {
        public string email { get; set; }
        public string newcod { get; set; }
        int min ;
        int seg =60;
        bool esperando;
        public Verificacion(string email)
        {
            InitializeComponent();
            this.email = email;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            newcod = "" + getRandomInteger(1000, 10000);
        }
        private async void btnverificar_Clicked(object sender, EventArgs e)
        {
           
        }

        private void btnotrocodigo_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtcodigo.Text)) 
            {
                string respuesta = "";
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    respuesta = await Controllers.AppController.setVerificaCuenta(txtcodigo.Text);
                }
                UserDialogs.Instance.HideLoading();
                if (respuesta == "CUENTA ACTIVADA")
                {
                    await Navigation.PushModalAsync(new Verificado());
                }
                else if (respuesta == "CUENTA NO ACTIVADA")
                {
                    await DisplayAlert("ALERTA", "CUENTA NO ACTIVADA!", "OK");
                }
                else if (respuesta == "CODIGO INVALIDO O NO EXISTE")
                {
                    await DisplayAlert("ALERTA", "CODIGO INVALIDO O NO EXISTE!", "OK");
                }
                else if (respuesta == "LA PAGINA NO RESPONDIO")
                {
                    await DisplayAlert("ALERTA", "LA PAGINA NO RESPONDIO!", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "INGRESA EL CODIGO!", "OK");
                txtcodigo.Focus();
            }
        }
        public int getRandomInteger(int minimum, int maximum)
        {
            return new Random().Next(minimum, maximum);
        }
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        { 
            //ENVIAR NUEVO CODIGO AL CORREO
            string estado = "";
            string respuesta = "";
            esperando = true;
            //ENVIAR EL NUEVO CODIGO A LA BASE DE DATOS

            //setNewCod
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(newcod))
            {
                ObjUsuario objj = new ObjUsuario { email = email, codigo = newcod };
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    respuesta = await Controllers.AppController.setNewCodigo(objj);
                }
                UserDialogs.Instance.HideLoading();
                if (respuesta == "CODIGO NO ACTUALIZADO")
                {
                    //correo no enviado
                    await DisplayAlert("ALERTA", "Fallo AL ACTUALIZAR SU CODIGO, Vuelvelo a intentar", "OK");
                }
                else if (respuesta == "CODIGO ACTUALIZADO")
                {
                    try
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress("noteskyhn@gmail.com");
                        mail.To.Add(email);
                        mail.Subject = "CODIGO DE VALIDACION DE CUENTA";
                        mail.Body = "CODIGO:" + newcod;

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
                        await DisplayAlert("INFO", "CODIGO ENVIADO DE NUEVO", "OK");
                        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {
                            seg--;

                            if (seg > 0)
                            {
                                esperando = true;
                                framebutton.IsEnabled = false;
                                btnotroCodigo.Text = "ESPERE..." + seg.ToString();
                            }
                            if (seg == 0)
                            {
                                //detener tiempo
                                esperando = false;
                                framebutton.IsEnabled = true;
                                btnotroCodigo.Text = "Volver a Enviar";
                            }
                            return esperando;
                        });
                    }
                    else if (estado.Contains("MAIL fallido"))
                    {
                        await DisplayAlert("ALERTA", "CODIGO NO ENVIADO!", "OK");
                    }
                }
                else if (respuesta == "USUARIO YA ACTIVADO")
                {
                    await DisplayAlert("INFO", "USUARIO YA HA SIDO ACTIVADO", "OK");
                }
                else if (respuesta == "USUARIO NO EXISTE")
                {
                    await DisplayAlert("ALERTA", "USUARIO NO EXISTE", "OK");
                }
                else if (respuesta == "LA PAGINA NO RESPONDIO")
                {
                    await DisplayAlert("ALERTA", "PAGINA NO RESPONDE", "OK");
                }
            }
            else
            {
                await DisplayAlert("ALERTA", "No puedes Actualizar Ocurrio un Error de datos", "OK");
                await Navigation.PopModalAsync();
            }
        }
    }
}