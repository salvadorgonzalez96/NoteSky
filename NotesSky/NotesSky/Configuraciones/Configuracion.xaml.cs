using Acr.UserDialogs;
using NotesSky.Models;
using NotesSky.Views;
using NotesSky.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Configuraciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage
    {
       string id_user { get; set; }
        public Configuracion(string id_user)
        {
            InitializeComponent();
            this.id_user = id_user;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            preCargaConfigs();
        }
        public async void preCargaConfigs() 
        {
            bool todobien = false;
            //cargar toda las configuracion en una lista de configuraciones
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                try
                {
                    Configuration.listC = await Controllers.AppController.getConfig(id_user);
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                }
                catch (Exception ex)
                {
                    //await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                }
                if (Configuration.listC.Count>0 || Configuration.listC!=null) { todobien = true; }
                else { todobien = false; }
            }
            UserDialogs.Instance.HideLoading();
            if (todobien == true)
            {
                foreach (ObjConfig config in Configuration.listC)
                {
                    if (config.nombre == "sincronizarNube")
                    {
                        //if (config.valor == "true") { sincronizar.IsToggled = true; }
                        //else { sincronizar.IsToggled = false; }
                    }
                    else if (config.nombre == "Estilo")
                    {
                        if (config.valor == "Predeterminado") { pickerStyle.SelectedIndex = 0; }
                        else if (config.valor == "Claro") { pickerStyle.SelectedIndex = 1; }
                        else if (config.valor == "Oscuro") { pickerStyle.SelectedIndex = 2; }
                    }
                    else if (config.nombre == "EstiloPagina")
                    {
                        if (config.valor == "red") { s1.BackgroundColor = Color.DarkRed; }
                        else if (config.valor == "MediumVioletRed") { s3.BackgroundColor = Color.DarkRed; }
                        else if (config.valor == "LightBlue") { s2.BackgroundColor = Color.DarkRed; }
                        else if (config.valor == "LightCoral") { s4.BackgroundColor = Color.DarkRed; }
                    }
                    else if (config.nombre == "Fuente")
                    {
                        if (config.valor == "Madani-Medium") { font1.IsChecked = true; }
                        else if (config.valor == "Madani-SemiBold") { font2.IsChecked = true; }
                        else if (config.valor == "Grotesque-Italic") { font3.IsChecked = true; }
                    }
                    else if (config.nombre == "SizeFont")
                    {
                        Control.Value = Convert.ToDouble(config.valor);
                    }
                }
            }
            else
            {
                //paso aki porque no tiene aun config guardada
            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageInformation());
        }

        private async void app_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageInformation());
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            test.Text = Control.Value.ToString("0.##");
            Control.ValueChanged += (send, args) =>
            {
                test2.FontSize = Control.Value;
            };
           
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                var res = await this.DisplayAlert("ALERTA!", "DESEA CERRAR SESION", "SI", "NO");
                if (res)
                {
                    bool persistenciafallo = false;
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Email", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Pass", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Nom", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Ids", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Login", "false");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    if (!persistenciafallo)
                    {
                        await this.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "PORFAVOR. Intentelo de nuevo!. \nOcurrio un Error de nuestro lado", "OK");
                    }
                }
            });
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void sincronizar_Toggled(object sender, ToggledEventArgs e)
        {
            //bool sincroniza = sincronizar.IsToggled;
            //if (sincroniza) 
            //{
            //    //Sincronizar con la nube se activo
            //    ObjConfig objj = new ObjConfig
            //    {
            //        idUser = id_user,
            //        nombre = "sincronizarNube",
            //        valor = "true"
            //    };
            //    using (UserDialogs.Instance.Loading("ESPERE..."))
            //    {
            //        await Controllers.AppController.setConfig(objj);
            //    }
            //    UserDialogs.Instance.HideLoading();
            //}
            //else 
            //{
            //    //se sincroniza con sqlite
            //    ObjConfig objj = new ObjConfig
            //    {
            //        idUser = id_user,
            //        nombre = "sincronizarNube",
            //        valor = "false"
            //    };
            //    using (UserDialogs.Instance.Loading("ESPERE..."))
            //    {
            //        await Controllers.AppController.setConfig(objj);
            //    }
            //    UserDialogs.Instance.HideLoading();
            //}
        }
        private async void pickerStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerStyle.SelectedIndex == -1)
            {
                //toco afuera del picker
            }
            else if (pickerStyle.SelectedIndex == 0)
            {
                string estado = "";
                //stylo predeterminado
                //se sincroniza con sqlite
                ObjConfig objj = new ObjConfig
                {
                    idUser = id_user,
                    nombre = "Estilo",
                    valor = "Predeterminado"
                };
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    estado=await Controllers.AppController.setConfig(objj);
                }
                UserDialogs.Instance.HideLoading();
            }
            else if (pickerStyle.SelectedIndex == 1)
            {
                string estado = "";
                //stylo claro
                ObjConfig objj = new ObjConfig
                {
                    idUser = id_user,
                    nombre = "Estilo",
                    valor = "Claro"
                };
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    estado = await Controllers.AppController.setConfig(objj);
                }
                UserDialogs.Instance.HideLoading();
            }
            else if (pickerStyle.SelectedIndex == 2)
            {
                string estado = "";
                //stylo oscuro
                ObjConfig objj = new ObjConfig
                {
                    idUser = id_user,
                    nombre = "Estilo",
                    valor = "Oscuro"
                };
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    estado = await Controllers.AppController.setConfig(objj);
                }
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void styloN1_Tapped(object sender, EventArgs e)
        {
            string estado = "";
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "EstiloPagina",
                valor = "red"
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
            s1.BackgroundColor = Color.DarkRed;
            s2.BackgroundColor = Color.White;
            s3.BackgroundColor = Color.White;
            s4.BackgroundColor = Color.White;
        }

        private async void styloN2_Tapped(object sender, EventArgs e)
        {
            string estado = "";
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "EstiloPagina",
                valor = "LightBlue"
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
            s1.BackgroundColor = Color.White;
            s2.BackgroundColor = Color.DarkRed;
            s3.BackgroundColor = Color.White;
            s4.BackgroundColor = Color.White;
        }

        private async void styloN3_Tapped(object sender, EventArgs e)
        {
            string estado = "";
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "EstiloPagina",
                valor = "MediumVioletRed"
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
            s1.BackgroundColor = Color.White;
            s2.BackgroundColor = Color.White;
            s3.BackgroundColor = Color.DarkRed;
            s4.BackgroundColor = Color.White;
        }

        private async void styloN4_Tapped(object sender, EventArgs e)
        {
            string estado = "";
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "EstiloPagina",
                valor = "LightCoral"
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
            s1.BackgroundColor = Color.White;
            s2.BackgroundColor = Color.White;
            s3.BackgroundColor = Color.White;
            s4.BackgroundColor = Color.DarkRed;
        }

        private async void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string estado = "";
            RadioButton radio = sender as RadioButton;
            var i = radio.Content;
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "Fuente",
                valor = ""+i
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
        }

        private async void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            string estado = "";
            RadioButton radio = sender as RadioButton;
            var i = radio.Content;
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "Fuente",
                valor = "" + i
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
        }

        private async void RadioButton_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            string estado = "";
            RadioButton radio = sender as RadioButton;
            var i = radio.Content;
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "Fuente",
                valor = "" + i
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
        }
        private async void btnactualizaFont_Clicked(object sender, EventArgs e)
        {
            string estado = "";
            ObjConfig objj = new ObjConfig
            {
                idUser = id_user,
                nombre = "SizeFont",
                valor = "" + Control.Value
            };
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                estado = await Controllers.AppController.setConfig(objj);
            }
            UserDialogs.Instance.HideLoading();
        }
    }
}