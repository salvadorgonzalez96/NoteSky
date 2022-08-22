using Acr.UserDialogs;
using NotesSky.Models;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Graphics;
using System.Drawing.Drawing2D;
using System.IO;
using Android.Util;
using Android.Graphics.Drawables;
using MediaManager;
using System.Reflection;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using System.Net.Http;
using Plugin.TextToSpeech;
using Rg.Plugins.Popup.Extensions;
using NotesSky.Views;

namespace NotesSky
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordatoriosVistaPrevia : ContentPage
    {
        public string id { get; set; }
        public static List<Notas> noti { get; set; }
        public string email { get; set; }
        public Byte[] audioFile { get; set; }
        public float[] audioFile2 { get; set; }
        public System.Drawing.Bitmap fotoaudios { get; set; }
        public string base64fotoAudio{ get; set; }
        public bool playingg = false;
        public string fileName { get; set; }
        public string fileNameshare { get; set; }
        public string fileNameFirmshare { get; set; }
        public Byte[] fotocarga { get; set; }
        public Byte[] fotocargaFirma { get; set; }
        Stream fileStreamPdf;
        public RecordatoriosVistaPrevia(string id,string email)
        {
            InitializeComponent();
            this.id = id;
            this.email = email;
            //this.Title = "ID: "+id;
            this.playingg = false;
            fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sample.mp3");
            fileNameshare = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "foto.png");
            fileNameFirmshare = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "firma.jpg");
        }
        public async void playing()
        {
            if (playingg)
            {
                playingg = false;
                var player = CrossMediaManager.Current.MediaPlayer;
                player.Stop();
            }
            else
            {
                playingg = true;
                await CrossMediaManager.Current.Play(fileName);
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool cargo = await TraeInfoNota(id);
            bool fallo = false;
            bool tienefoto = false;
            bool tieneaudio = false;
            bool tienefirma = false;
            ImageSource imageSource=null;
            ImageSource imageSource2=null;
            ImageSource imageFSource=null;
            //int idnota = int.Parse(id)-1;
            if (cargo)
            {
                Notas nta = noti[0];
                try
                {
                    nombreNota.Text = nta.titulo;
                    FechaNota.Text = nta.fecha.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    contenidoNota.Text = nta.contenido;
                    FechaUltMod.Text = nta.fecha_ULT_mod.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    fallo = false;
                }
                catch (Exception ex)
                {
                    fallo = true;
                    await DisplayAlert("ALERTA","ALGO FALLO DE NUESTRA PARTE. INTENTALO DE NUEVO","OK");
                }
                if (!fallo)
                {
                    try
                    {
                        fotocarga = Convert.FromBase64String(nta.foto);
                        System.IO.Stream stream = new System.IO.MemoryStream(fotocarga);
                        imageSource = ImageSource.FromStream(() => stream);
                        tienefoto = true;
                    }
                    catch (Exception ex)
                    {
                        tienefoto = false;
                    }

                    try
                    {
                        audioFile = Convert.FromBase64String(nta.audio);
                        System.IO.Stream streamss = new System.IO.MemoryStream(audioFile);

                        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write)) { streamss.CopyTo(fileStream); }

                        string resp = "";
                        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                        {
                            resp = await Controllers.AppController.getWaves(email, int.Parse(id));
                            if (resp != "no obtenido")
                            {
                                var ImagenSave2 = Convert.FromBase64String(resp);
                                System.IO.Stream streams = new System.IO.MemoryStream(ImagenSave2);
                                imageSource2 = ImageSource.FromStream(() => streams);
                            }
                            else
                            {
                                fotoaudio.Source = "soundW.png";
                            }
                        }

                        tieneaudio = true;
                    }
                    catch (Exception ex)
                    {
                        tieneaudio = false;
                    }

                    try
                    {
                        fotocargaFirma = Convert.FromBase64String(nta.firma);
                        System.IO.Stream stream1 = new System.IO.MemoryStream(fotocargaFirma);
                        imageFSource = ImageSource.FromStream(() => stream1);

                        tienefirma = true;
                    }
                    catch (Exception ex)
                    {
                        tienefirma = false;
                    }

                    if (tieneaudio)
                    {
                        if (audioFile.Length > 1) { 
                            frameaudio.IsVisible = true;
                            fotoaudio.Source = imageSource2;
                        }
                    }
                    if (tienefoto)
                    {
                        if (fotocarga.Length > 1)
                        {
                            framefoto.IsVisible = true;
                            imsnota.Source = imageSource;
                        }
                    }
                    if (tienefirma)
                    {
                        if (fotocargaFirma.Length>1)
                        {
                            framefirma.IsVisible = true;
                            fotoFirma.Source = imageFSource;
                        }
                        
                    }
                }
                //using (var stream3 = new MemoryStream())
                //{
                //    //fotoaudios.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream3);
                //    var bytes = ImageToByte(fotoaudios);
                //    base64fotoAudio = Convert.ToBase64String(bytes);
                //}

            }
        }
        public float[] FloatArrayFromByteArray(byte[] input)
        {
            float[] output = new float[input.Length / 4];
            Buffer.BlockCopy(input, 0, output, 0, input.Length);
            return output;
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Android.Media.Image item = sender as Android.Media.Image;
            //IMAGEN 
            //try
            //{
            //    await Share.RequestAsync(new ShareTextRequest
            //    {
            //        Title = "FOTO",
            //        Text="AKI VA LA FOTO COMO ShareFile",
            //        Subject="ENVIAR FOTO",
            //        Uri="http://www.google.hn"
            //    });
            //}
            //catch (Exception) 
            //{
            //    await DisplayAlert("ERROR", "ERROR AL COMPARTIR LA FOTO", "OK");
            //}
            Stream stream = new MemoryStream(fotocarga);
            using (var fileStream = new FileStream(fileNameshare, FileMode.Create, FileAccess.Write)) { stream.CopyTo(fileStream); }
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(fileNameshare),
                PresentationSourceBounds = DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet
                            ? new System.Drawing.Rectangle(0, 20, 0, 0)
                            : System.Drawing.Rectangle.Empty
            });
        }
        private async void tapfirma_Tapped(object sender, EventArgs e)
        {
            Stream stream = new MemoryStream(fotocargaFirma);
            using (var fileStream = new FileStream(fileNameFirmshare, FileMode.Create, FileAccess.Write)) { stream.CopyTo(fileStream); }
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(fileNameFirmshare),
                PresentationSourceBounds = DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet
                            ? new System.Drawing.Rectangle(0, 20, 0, 0)
                            : System.Drawing.Rectangle.Empty
            });
        }
        public async Task<bool> TraeInfoNota(string id)
        {
            bool loConsegui = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getNotas2(email, int.Parse(id)) != null)
                    {
                        list = await Controllers.AppController.getNotas2(email, int.Parse(id));
                        if (list.Count >= 1)
                        {
                            List<Notas> collection = new List<Notas>(list);
                            noti = collection;
                            loConsegui = true;
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                await DisplayAlert("ALERTA","NO SE CARGO TU NOTA.\n CONECTATE A INTERNET!","OK");
            }
            return loConsegui;
        }
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //AUDIO
            playing();
            //try
            //{
            //    await Share.RequestAsync(new ShareTextRequest
            //    {
            //        Title = "AUDIO",
            //        Text = "AKI VA EL AUDIO",
            //        Subject = "ENVIAR AUDIO",
            //        Uri = "http://www.audio.hn"
            //    });
            //}
            //catch (Exception)
            //{
            //    await DisplayAlert("ERROR", "ERROR AL COMPARTIR EL VIDEO", "OK");
            //}
        }
        private async void btnedit_Clicked(object sender, EventArgs e)
        {
            Object nota = new
            {
                id = 1,
                titulo = "NOTA1",
                contenido = "HOLA COMO ESTAS AMIGO AKI MOSTRANDO LA NOTA NUMERO 1",
                tengoAudio = false,
                tengoFoto = false
            };
            RecordatoriosEditar pag = new RecordatoriosEditar();
            pag.BindingContext = nota;
            //demostracion enviando id de recordatorio
            await Navigation.PushAsync(pag);
        }

        private void btnview_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            string r = await setestaurar(id, email, "INACTIVO");
            if (r == "ACTUALIZADO")
            {
                await DisplayAlert("INFO", "Nota Eliminada", "OK");
            }
            else
            {
                await DisplayAlert("INFO", "Nota no Eliminada", "OK");
            }
        }

        private async void btnaddToFav_Clicked(object sender, EventArgs e)
        {
            string r = await setestaurar2(id, email, "1");
            if (r == "ACTUALIZADO")
            {
                await DisplayAlert("INFO", "Nota Agregada a Favoritos", "OK");
            }
            else
            {
                await DisplayAlert("INFO", "Nota Agregada", "OK");
            }
        }
        public async Task<string> setestaurar(string nota, string email, string estados)
        {
            string estado = "";
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    string list = "";
                    if (await Controllers.AppController.setRestaurar(nota, email, estados) != null)
                    {
                        list = await Controllers.AppController.setRestaurar(nota, email, estados);
                        if (list == "ACTUALIZADO")
                        {
                            estado = "ACTUALIZADO";
                        }
                        else
                        {
                            estado = "No ACTUALIZADO";
                        }
                    }
                    else
                    {
                        estado = "No ACTUALIZADO";
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                estado = "INCORRECTO";
            }
            return estado;
        }
        public async Task<string> setestaurar2(string nota, string email, string estados)
        {
            string estado = "";
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    string list = "";
                    if (await Controllers.AppController.setRestaurarFav(nota, email, estados) != null)
                    {
                        list = await Controllers.AppController.setRestaurarFav(nota, email, estados);
                        if (list == "ACTUALIZADO")
                        {
                            estado = "ACTUALIZADO";
                        }
                        else
                        {
                            estado = "No ACTUALIZADO";
                        }
                    }
                    else
                    {
                        estado = "No ACTUALIZADO";
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                estado = "INCORRECTO";
            }
            return estado;
        }
        private void Label_Focused(object sender, FocusEventArgs e)
        {
            //scrollview.ScrollToAsync(0, 0, true);
        }
        public static byte[] ImageToByte(System.Drawing.Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        

        private async void fotoPdfs_Tapped(object sender, EventArgs e)
        {
            //string resp = "";
            //System.IO.Stream streams;
            //if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //{
            //    resp = await Controllers.AppController.getPdf(email, int.Parse(id));
            //    if (resp != "no obtenido")
            //    {
            //        var PdfSave2 = Convert.FromBase64String(resp);
            //        streams = new System.IO.MemoryStream(PdfSave2);
            //        //var pdfSource2 = ImageSource.FromStream(() => streams);
            //        //fotoaudio.Source = pdfSource2;


            //    }
            //    else
            //    {
            //        fotoPdf.Source = "pdf2.png";
            //    }
            //}
            //var http = new HttpClient();
            //var ss = await http.GetStreamAsync("https://gerald.verslu.is/subscribe.pdf");
            //using (var memmorystream = new MemoryStream())
            //{
            //    await ss.CopyToAsync(memmorystream);
            //    await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("sample.pdf", "application/pdf", memmorystream, PDFOpenContext.InApp);
            //}

        }

        private async void btnescuchar_Clicked(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak(contenidoNota.Text);
        }
    }
}