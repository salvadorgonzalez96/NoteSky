using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Extensions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using NotesSky.Views;
using System.IO;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupCamera : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Image img { get; set; }
        public Entry entry { get; set; }
        public bool tomefoto { get; set; }
        public Byte[] ImagenSave { get; set; }
        public string ventana { get; set; }
        public Action action { get; set; }
        public Action action2 { get; set; }
        public PopupCamera(Image img,Entry entry, bool tomefoto, Byte[] ImagenSave, string ventana,Action action, Action action2)
        {
            InitializeComponent();
            this.img = img;
            this.entry = entry;
            this.tomefoto = tomefoto;
            this.ImagenSave = ImagenSave;
            this.ventana = ventana;
            this.action = action;
            this.action2 = action2;
        }

        private async void btngalery_Clicked(object sender, EventArgs e)
        {
            if (ventana == "registro")
            {
                action();
            }
            else
            {
                action();
            }
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
            //        img.Source = ImageSource.FromStream(() => { return file.GetStream(); });
            //        ImagenSave = null;
            //        ImagenSave = File.ReadAllBytes(file.Path);
            //        if (entry != null)
            //        {
            //            entry.Text = "" + Path.GetFileName(file.Path);
            //        }
            //        await Navigation.PopPopupAsync();
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
        }

        private async void btnCamera_Clicked(object sender, EventArgs e)
        {
            if (ventana == "registro")
            {
                action2();
            }
            else
            {
                action2();
            }
            
        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}