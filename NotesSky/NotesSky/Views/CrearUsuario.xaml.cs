using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using NotesSky.Models;
using NotesSky.Views;
using NotesSky.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearUsuario : ContentPage
    {
        public byte[] ImagenSave { get; set; }
        public bool tomefoto { get; set; }
        public string textoCondiciones { get; set; }
        string codrandom = "";
        public CrearUsuario()
        {
            InitializeComponent();
            //lblCondition.Text = "<span style='color: black;'>Al Crear tu Usuario. estas de acuerdo con Nuestros </span><span style='color: blue;'>Términos & Condiciones y Politicas de Privacidad</span>";
            List<String> list = new List<string>();
            list.Add("Masculino");
            list.Add("Femenino");
            btngenero.ItemsSource = list;
            tomefoto = false;
        }
        private async void btncrear_Clicked(object sender, EventArgs e)
        {
            
        }
        private void btnElegirimagen_Clicked(object sender, EventArgs e)
        {

        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void btnGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Verificacion(""));
        }
        public async void Fototake()
        {
            try
            {
                var TomarFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Pictures",
                    Name = DateTime.Now.ToString() + "_IMG.jpg",
                    SaveToAlbum = true,
                    CompressionQuality = 50
                });

                if (TomarFoto != null)
                {
                    ImagenSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    TomarFoto.GetStream().CopyTo(memoryStream);
                    ImagenSave = null;
                    ImagenSave = memoryStream.ToArray();
                    tomefoto = true;
                    UbiImagen.Source = ImageSource.FromStream(() => { return TomarFoto.GetStream(); });
                    uri_path.Text = "" + Path.GetFileName(TomarFoto.Path);
                    await Navigation.PopPopupAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No Se Puede Tomar Fotografias", "OK");
            }
        }
        public async void FotoGalery()
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                    });

                    if (file == null)
                        return;
                    tomefoto = true;
                    UbiImagen.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                    ImagenSave = null;
                    ImagenSave = File.ReadAllBytes(file.Path);
                    uri_path.Text = "" + Path.GetFileName(file.Path);
                    await Navigation.PopPopupAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("ALERTA", "ERROR AL CARGAR LA FOTO.", "OK");
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("ALERTA", "ERROR AL CARGAR LA FOTO.", "OK");
            }
        }
        private async void btnSavefoto_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopupCamera(UbiImagen, uri_path,tomefoto,ImagenSave, "registro", FotoGalery, Fototake));
            //DESDE GALERIA
            //try
            //{
            //    if (CrossMedia.Current.IsPickPhotoSupported)
            //    {
            //        var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            //        {
            //            PhotoSize = PhotoSize.Medium,
            //        });

            //        if (file == null)
            //            return;
            //        tomefoto = true;
            //        UbiImagen.Source = ImageSource.FromStream(() => { return file.GetStream(); });
            //        ImagenSave = File.ReadAllBytes(file.Path);
            //        uri_path.Text = "" + Path.GetFileName(file.Path);
            //    }
            //    else
            //    {
            //        await Application.Current.MainPage.DisplayAlert("ALERTA", "ERROR AL CARGAR LA FOTO.", "OK");
            //    }
            //}
            //catch (Exception)
            //{
            //    await Application.Current.MainPage.DisplayAlert("ALERTA", "ERROR AL CARGAR LA FOTO.", "OK");
            //}
            //DESDE CAMARA
            //try
            //{
            //    var TomarFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //    {
            //        Directory = "Pictures",
            //        Name = DateTime.Now.ToString() + "_IMG.jpg",
            //        SaveToAlbum = true,
            //        CompressionQuality = 50
            //    });

            //    if (TomarFoto != null)
            //    {
            //        ImagenSave = null;
            //        MemoryStream memoryStream = new MemoryStream();

            //        TomarFoto.GetStream().CopyTo(memoryStream);
            //        ImagenSave = memoryStream.ToArray();
            //        tomefoto = true;
            //        UbiImagen.Source = ImageSource.FromStream(() => { return TomarFoto.GetStream(); });
            //        uri_path.Text = ""+Path.GetFileName(TomarFoto.Path);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", "No Se Puede Tomar Fotografias", "OK");
            //}
        }
        private void limpiar()
        {
            uri_path.Text="";
            txtnombre.Text="";
            txtapellido.Text="";
            btngenero.SelectedIndex=0;
            txttelefono.Text = "";
            txtciudad.Text="";
            txtcorreo.Text="";
            txtcontra.Text="";
            tomefoto = false;
            ImagenSave = null;
            UbiImagen.Source = "user.png";
        }
        private async Task<bool> validarCampos()
        {
            Boolean validados = true;
            bool isDigitPresent = txtnombre.Text.Any(c => char.IsDigit(c));
            bool isDigitPresent2 = txtapellido.Text.Any(c2 => char.IsDigit(c2));

            if (String.IsNullOrEmpty(uri_path.Text))//IMAGEN VACIA
            {
                await DisplayAlert("ALERTA", "Selecciona una Foto!", "OK");
                validados = false;
            }
            else if (String.IsNullOrEmpty(txtnombre.Text))
            {
                await DisplayAlert("ALERTA", "Escriba su Nombre!", "OK");
                validados = false;
                txtnombre.Focus();
            }
            else if (isDigitPresent)
            {
                await DisplayAlert("ALERTA", "Su Nombre no puede contener Numeros!", "OK");
                validados = false;
                txtnombre.Focus();
            }
            else if (String.IsNullOrEmpty(txtapellido.Text))
            {
                await DisplayAlert("ALERTA", "Escriba sus Apellidos!", "OK");
                validados = false;
                txtapellido.Focus();
            }
            else if (isDigitPresent2)
            {
                await DisplayAlert("ALERTA", "Su Apellido no puede contener Numeros!", "OK");
                validados = false;
                txtapellido.Focus();
            }
            else if (btngenero.SelectedIndex == -1 || btngenero.SelectedItem.ToString()=="Seleccione su Género")
            {
                await DisplayAlert("ALERTA", "Seleccione su Genero!", "OK");
                validados = false;
                btngenero.Focus();
            }
            else if (String.IsNullOrEmpty(txttelefono.Text))
            {
                await DisplayAlert("ALERTA", "Escriba su numero telefonico!", "OK");
                validados = false;
                txttelefono.Focus();
            }
            else if (String.IsNullOrEmpty(txtciudad.Text))
            {
                await DisplayAlert("ALERTA", "Ingrese el nombre de su ciudad!", "OK");
                validados = false;
                txttelefono.Focus();
            }
            else if (String.IsNullOrEmpty(txtcorreo.Text))
            {
                await DisplayAlert("ALERTA", "Ingrese su correo electronico!", "OK");
                validados = false;
                txtcorreo.Focus();
            }
            else if (!txtcorreo.Text.Contains("@") || !txtcorreo.Text.Contains("."))
            {
                await DisplayAlert("ALERTA", "Correo Invalido!", "OK");
                validados = false;
                txtcorreo.Focus();
            }
            else if (String.IsNullOrEmpty(txtcontra.Text))
            {
                await DisplayAlert("ALERTA", "Ingrese su Contraseña!", "OK");
                validados = false;
                txtcontra.Focus();
            }
            else
            {

            }

            return validados;
        }
        //metodo que genera numeros aleatorios
        public int getRandomInteger(int minimum, int maximum) 
        { 
            return new Random().Next(minimum,maximum); 
        }

        private async void btncrea_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (await validarCampos())
                {
                    codrandom = "" + getRandomInteger(1000, 10000);
                    CrearCliente(codrandom);
                }
            }
            catch (Exception exception)
            {
                await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE, VUELVELO A INTENTAR!", "OK");
            }
        }

        private async void CrearCliente(string codrandom)
        {
            string respuesta = "";
            ObjUsuario objj = new ObjUsuario
            {
                id = 0,
                nombre = txtnombre.Text,
                apellido = txtapellido.Text,
                genero = btngenero.SelectedItem.ToString(),
                tel = txttelefono.Text,
                ciudad = txtciudad.Text,
                fecha = datetxt.Date.ToString("dd/MM/yyyy"),
                email = txtcorreo.Text,
                contraseña = Config.Ecript.Encrypt(txtcontra.Text),
                img = Convert.ToBase64String(ImagenSave, 0, ImagenSave.Length),
                tipo = "",
                codigo = codrandom,
                estado = ""
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                respuesta=await Controllers.AppController.setUser(objj,codrandom);
            }
            UserDialogs.Instance.HideLoading();
            if (respuesta == "MAIL fallido") 
            {
                //correo no enviado
                await DisplayAlert("ERROR", "Fallo AL enviar tu Codigo, Vuelvelo a intentar al iniciar sesion", "OK");
            }
            else if(respuesta == "CORREO ENVIADO")
            {
                await DisplayAlert("INFO", "USUARIO CREADO", "OK");
                limpiar();
                await Navigation.PopModalAsync();
            }
            else if (respuesta == "USUARIO EXISTENTE")
            {
                await DisplayAlert("INFO", "USUARIO EXISTENTE", "OK");
            }
            else if (respuesta == "USER no creado") 
            {
                await DisplayAlert("ALERTA", "USUARIO NO CREADO", "OK");
            }
            else if (respuesta == "FALLO INSERTAR")
            {
                await DisplayAlert("ALERTA", "USUARIO NO CREADO", "OK");
            }
            else if (respuesta == "LA PAGINA NO RESPONDIO")
            {
                await DisplayAlert("ALERTA", "PAGINA NO RESPONDE", "OK");
            }
        }

        private void clickPoliticas_Tapped(object sender, EventArgs e)
        {
            Browser.OpenAsync(Config.Configuration.getPagePoliticas);
        }
    }
}