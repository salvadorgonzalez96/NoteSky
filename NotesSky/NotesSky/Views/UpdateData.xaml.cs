using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NotesSky.Models;
using Acr.UserDialogs;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Extensions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateData : ContentPage
    {
        public byte[] ImagenSave { get; set; }
        public static byte[] ImagenSave2 { get; set; }
        public bool tomefoto { get; set; }
        string Iid { get; set; }
        string Iemail { get; set; }
        bool apareci=false;
        public UpdateData(string id,string email)
        {
            InitializeComponent();
            this.Iid = id;
            this.Iemail = email;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            bool todobien = false;
            if (apareci ==false)
            {
                apareci = true;
                //LLENADO AL INICIO DE CONTROLES
                imgfoto.Source = "emptyImage.png";
                List<String> list = new List<string>();
                list.Add("Masculino");
                list.Add("Femenino");
                btngenero.ItemsSource = list;
                //RECUPERAR BINDING DESDE LA BASE DE DATOS
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    ObjUsuario user = new ObjUsuario();
                    using (UserDialogs.Instance.Loading("ESPERE..."))
                    {
                        try
                        {
                            user = await Controllers.AppController.getUser(Iid);
                        }
                        catch (System.Net.Sockets.SocketException ex)
                        {
                            await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                        }
                        if (!String.IsNullOrEmpty(user.nombre)) { todobien = true; }
                        else { await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK"); todobien = false; }
                    }
                    UserDialogs.Instance.HideLoading();

                    if (todobien)
                    {
                        txtid.Text = "" + user.id;
                        txtnombre.Text = "" + user.nombre;
                        txtapellido.Text = "" + user.apellido;
                        //txtusuario.Text = user.usuario;
                        btngenero.SelectedItem = user.genero;
                        txtcorreo.Text = "" + user.email;
                        txtcontra.Text = "";
                        txtciudad.Text = "" + user.ciudad;
                        string tell = "" + user.tel;
                        string str1 = tell.Replace("-", "");
                        string str2 = str1.Replace("+", "");
                        string str3 = str2.Trim();
                        string str4 = System.Text.RegularExpressions.Regex.Replace(str3, @"\s+", " ");
                        txttelefono.Text = "" + str4.Trim();
                        ImagenSave = Convert.FromBase64String(user.img);
                        System.IO.Stream stream = new System.IO.MemoryStream(ImagenSave);
                        var imageSource = ImageSource.FromStream(() => stream);
                        imgfoto.Source = imageSource;
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "DEBES CONECTARTE A LA RED", "OK");
                    await Navigation.PopModalAsync();
                    UserDialogs.Instance.HideLoading();
                }
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

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
                    imgfoto.Source = ImageSource.FromStream(() => { return TomarFoto.GetStream(); });
                    //entry.Text = "" + Path.GetFileName(TomarFoto.Path);
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
                    imgfoto.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                    ImagenSave = null;
                    ImagenSave = File.ReadAllBytes(file.Path);
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
        private async void TapImageUpdate_Tapped(object sender, EventArgs e)
        {
            //selecciona foto de galeria o camara
            await Navigation.PushPopupAsync(new PopupCamera(imgfoto, null, tomefoto, ImagenSave,"update",FotoGalery,Fototake));
        }
        private async Task<bool> validarCampos()
        {
            Boolean validados = true;
            bool isDigitPresent = txtnombre.Text.Any(c => char.IsDigit(c));
            bool isDigitPresent2 = txtapellido.Text.Any(c2 => char.IsDigit(c2));

            if (ImagenSave==null)//IMAGEN VACIA
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
            else if (btngenero.SelectedIndex == -1 || btngenero.SelectedItem.ToString() == "Seleccione su Género")
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
            //else if (String.IsNullOrEmpty(txtcontra.Text))
            //{
            //    await DisplayAlert("ALERTA", "Ingrese su Contraseña!", "OK");
            //    validados = false;
            //    txtcontra.Focus();
            //}
            else
            {

            }

            return validados;
        }
        private async void btnactualiza_Tapped(object sender, EventArgs e)
        {
            string response = "";
            bool estado = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    if (await validarCampos())
                    {
                        try
                        {
                            string Pass = "";
                            string IMG64 = Convert.ToBase64String(ImagenSave, 0, ImagenSave.Length);
                            if (String.IsNullOrEmpty(txtcontra.Text)) { Pass = null; }
                            else { Pass = Config.Ecript.Encrypt(txtcontra.Text); }
                            ObjUsuario obj = new ObjUsuario
                            {
                                email = txtcorreo.Text,
                                img = IMG64,
                                nombre = txtnombre.Text,
                                apellido = txtapellido.Text,
                                genero = btngenero.SelectedItem.ToString(),
                                contraseña = Pass,
                                ciudad = txtciudad.Text,
                                tel = txttelefono.Text
                            };
                            try
                            {
                                response = await Controllers.AppController.setUpdateUser(obj);
                            }
                            catch (System.Net.Sockets.SocketException ex)
                            {
                                await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                            }
                            //if (!String.IsNullOrEmpty(user.nombre)) { todobien = true; }
                            //else { await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK"); todobien = false; }
                            if (response == "ACTUALIZADO")
                            {
                                estado = true;
                            }
                            else if (response == "NO ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "1 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "2 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "3 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "4 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "5 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "6 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else if (response == "7 No ACTUALIZADO")
                            {
                                estado = false;
                            }
                            else
                            {
                                estado = false;
                            }
                            if (estado)
                            {
                                limpiar();
                                await DisplayAlert("INFO", "USUARIO ACTUALIZADO CORRECTAMENTE!", "OK");
                                await Navigation.PopModalAsync();
                            }
                            else
                            {
                                await DisplayAlert("ERROR", "NO SE PUDO ACTUALIZAR LA INFORMACION!\nINTENTALO DE NUEVO", "OK");
                                txtnombre.Focus();
                            }

                        }
                        catch (Exception exception)
                        {
                            await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE, VUELVELO A INTENTAR!", "OK");
                        }
                    }
                }
                catch (Exception exception)
                {
                    await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE, VUELVELO A INTENTAR!", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "DEBES CONECTARTE A LA RED", "OK");
            }
        }

        private void limpiar()
        {
            txtnombre.Text = "";
            txtapellido.Text = "";
            btngenero.SelectedIndex = 0;
            txttelefono.Text = "";
            txtciudad.Text = "";
            txtcorreo.Text = "";
            txtcontra.Text = "";
            tomefoto = false;
            ImagenSave = null;
            imgfoto.Source = "emptyImage.png";
        }
    }
}