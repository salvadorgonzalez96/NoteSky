using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Plugin.Media;
using System.IO;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopupAdjunta2 : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Frame image { get; set; }
        public Frame dibujo { get; set; }
        public Frame dibujopng { get; set; }
        public Frame framerecordaudio { get; set; }
        public Frame framefileaudio { get; set; }
        public SignaturePad.Forms.SignaturePadView pad{ get; set; }
        public Image img{ get; set; }
        public static byte[] ImagenSave { get; set; }
        public bool tomefoto { get; set; }
        
        public MyPopupAdjunta2(Frame framerecordaudio, Frame framefileaudio,Frame dibujopng,Frame dibujo,Frame img,SignaturePad.Forms.SignaturePadView pad,Image imgg, Byte[] imgbytes)
        {
            InitializeComponent();
            this.image = img;
            this.dibujo = dibujo;
            this.dibujopng = dibujopng;
            this.pad = pad;
            this.img = imgg;
            this.framefileaudio = framefileaudio;
            this.framerecordaudio = framerecordaudio;
            ImagenSave = imgbytes;
        }
        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
        private void findImage_Tapped(object sender, EventArgs e)
        {
            if (ImagenSave != null && image.IsVisible)//si la imagen fue cargada y el frame esta activo
            {
                image.IsVisible = false;
            }
            else if (ImagenSave==null && image.IsVisible)//si no se cargo ninguna foto pero el frame esta activo
            {
                FotoGalery();
            }
            else//si el frame no esta activo
            {
                image.IsVisible = true;
                FotoGalery();
            }
        }
        private void findCamer_Tapped(object sender, EventArgs e)
        {

            if (ImagenSave != null && image.IsVisible)//si la imagen fue cargada y el frame esta activo
            {
                image.IsVisible = false;
            }
            else if (ImagenSave == null && image.IsVisible)//si no se cargo ninguna foto pero el frame esta activo
            {
                Fototake();
            }
            else
            {
                image.IsVisible = true;
                Fototake();
            }
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
                    RecordatoriosNew.imgbytes= memoryStream.ToArray();
                    tomefoto = true;
                    img.Source = ImageSource.FromStream(() => { return TomarFoto.GetStream(); });
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
                    img.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                    ImagenSave = null;
                    ImagenSave = File.ReadAllBytes(file.Path);
                    RecordatoriosNew.imgbytes = File.ReadAllBytes(file.Path);
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

        private void btnDibujo_Clicked(object sender, EventArgs e)
        {
            if (dibujopng.IsVisible)
            {
                dibujopng.IsVisible = false;
                if (dibujo.IsVisible)
                {
                    dibujo.IsVisible = false;
                }
                else
                {
                    dibujo.IsVisible = true;
                }
            }
            else
            {
                if (dibujo.IsVisible)
                {
                    dibujo.IsVisible = false;
                }
                else
                {
                    dibujo.IsVisible = true;
                }
            }
        }
        async Task<FileResult> PickAndShow(PickOptions options)
        {
            
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    //Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("mp3", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        var memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);

                        // Convert Stream To Array
                        byte[] byteArray = memoryStream.ToArray();

                        //Image = ImageSource.FromStream(() => stream);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }

        private async void btnAudio_Clicked(object sender, EventArgs e)
        {
            if (framefileaudio.IsVisible) { framefileaudio.IsVisible = false; }
            else
            {
                framefileaudio.IsVisible = true;
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "audio/x-wav", "audio/x-ms-wma", "audio/x-midi", "audio/x-ogg", "audio/x-aac", "audio/x-mpeg3", "audio/x-mpeg", "audio/x-mpeg3", "audio/x-mp3", "audio/mp3", "audio/mpeg3", "audio/mpeg" } }
                });
                var options = new PickOptions
                {
                    PickerTitle = "Selecciona tu audio",
                    FileTypes = customFileType,
                };
                await PickAndShow(options);
            }
        }

        private void btnpicture_Clicked(object sender, EventArgs e)
        {
            if (ImagenSave != null && image.IsVisible)//si la imagen fue cargada y el frame esta activo
            {
                image.IsVisible = false;
            }
            else if (ImagenSave == null && image.IsVisible)//si no se cargo ninguna foto pero el frame esta activo
            {
                FotoGalery();
            }
            else
            {
                image.IsVisible = true;
                FotoGalery();
            }
        }

        private void btnpic_Clicked(object sender, EventArgs e)
        {
            if (ImagenSave != null && image.IsVisible)//si la imagen fue cargada y el frame esta activo
            {
                image.IsVisible = false;
            }
            else if (ImagenSave == null && image.IsVisible)//si no se cargo ninguna foto pero el frame esta activo
            {
                Fototake();
            }
            else
            {
                image.IsVisible = true;
                Fototake();
            }
        }

        private void btnVoz_Clicked(object sender, EventArgs e)
        {
            if (framerecordaudio.IsVisible)
            {
                framerecordaudio.IsVisible = false;
            }
            else
            {
                framerecordaudio.IsVisible = true;
            }
        }
    }
}