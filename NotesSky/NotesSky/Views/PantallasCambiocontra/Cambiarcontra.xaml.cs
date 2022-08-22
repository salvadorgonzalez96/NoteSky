using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NotesSky.Models;
using NotesSky.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views.PantallasCambiocontra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cambiarcontra : ContentPage
    {
        public string email { get; set; }
        public Cambiarcontra(string email)
        {
            InitializeComponent();
            this.email = email;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private async void btncambiar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PantallaLogin());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void txtconfirmclave_TextChanged(object sender, TextChangedEventArgs e)
        {
            int tam = txtconfirmclave.Text.Length;
            if (txtnuevaclave.Text.Length == tam) 
            {
                if (txtconfirmclave.Text == txtnuevaclave.Text) 
                {
                    txtvalidatext.IsVisible = false;
                }
                else 
                {
                    txtvalidatext.IsVisible = true;
                }
            }
            else
            {
                txtvalidatext.IsVisible = true;
            }
        }

        private async void btncambiaPass_Tapped(object sender, EventArgs e)
        {
            //VALIDAR QUE LAS CLAVES SEAN IGUALES Y ENVARLA AL SERVIDOR LUEGO MANDARLO A EL LOGIN
            if (txtconfirmclave.Text == txtnuevaclave.Text)
            {
                string respuesta = "";
                ObjUsuario objj = new ObjUsuario
                {
                    email = email,
                    contraseña = Config.Ecript.Encrypt(txtnuevaclave.Text)
                };
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    respuesta = await Controllers.AppController.setPass(objj);
                }
                UserDialogs.Instance.HideLoading();
                if (respuesta == "NO ACTUALIZADO")
                {
                    //correo no enviado
                    await DisplayAlert("ALERTA", "CLAVE NO CAMBIADA", "OK");
                }
                else if (respuesta == "ACTUALIZADO")
                {
                    await DisplayAlert("INFO", "CLAVE CAMBIADA CORRECTAMENTE", "OK");
                    await Navigation.PushModalAsync(new PantallaLogin());
                }
                else if (respuesta == "INGRESE UNA CONTRASEÑA")
                {
                    await DisplayAlert("ALERTA", "0X1 ALGO FALLO\n INTENTELO DE NUEVO", "OK");
                }
                else if (respuesta == "INGRESE UNA CORREO")
                {
                    await DisplayAlert("ALERTA", "0X2 ALGO FALLO\n INTENTELO DE NUEVO", "OK");
                }
                else if (respuesta == "CONTRASEÑA NO CAMBIADA")
                {
                    await DisplayAlert("ALERTA", "0X3 ALGO FALLO\n INTENTELO DE NUEVO", "OK");
                }
                else if (respuesta == "LA PAGINA NO RESPONDIO")
                {
                    await DisplayAlert("ALERTA", "0X4 PAGINA NO RESPONDIO\n INTENTELO DE NUEVO", "OK");
                }
            }
            else
            {
                await DisplayAlert("ALERTA","LAS CLAVES NO COINCIDEN ","OK");
            }
        }
    }
}