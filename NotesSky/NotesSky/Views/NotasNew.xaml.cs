using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.AudioRecorder;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MediaManager;
using Acr.UserDialogs;
using NotesSky.Models;
using SignaturePad.Forms;
using NotesSky.Configuraciones;

namespace NotesSky
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotasNew : ContentPage
    {
        public string id { get; set; }
        AudioRecorderService recorder;
        public string email { get; set; }
        public double tam { get; set; }
        public string fileName { get; set; }
        bool isTimerRunning = false;
        bool isTimer2Running = false;
        bool seReprodujo = false;
        int seconds = 0, minutes = 0;
        public static Byte[] imgbytes { get; set; }
        public static Byte[] audiobytes { get; set; }

        public NotasNew(string id,string email)
        {
            InitializeComponent();
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "samples.wav");
            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(180)//3 minutos
            };
            SearchBarTextChanged += HandleSearchBarTextChanged;
            this.id = id;
            this.email = email;
            tam = Application.Current.MainPage.Height;
            this.BindingContext = this;
        }
        public event EventHandler<string> SearchBarTextChanged;
        //void ISearchPage.OnSearchBarTextChanged((string text) => SearchBarTextChanged?.Invoke(this, text);
        void HandleSearchBarTextChanged(object sender, string searchBarText)
        {
            //Logic to handle updated search bar text
        }
        private void Back_Click(object sender, EventArgs e)
        {

        }

        private void Click_More(object sender, EventArgs e)
        {

        }

        private async void Adjunta_Click(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new Views.MyPopupAdjunta(frameaudio, framefileaudio, framedibujoPng, framedibujo,frameimagen,firmita,imsnota, imgbytes));
        }

        private void Moreclick(object sender, EventArgs e)
        {

        }

        private void Shareclick(object sender, EventArgs e)
        {

        }

        private void toFavClick(object sender, EventArgs e)
        {

        }

        public void OnSearchBarTextChanged(string text)
        {
            throw new NotImplementedException();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Configuracion(id));
        }

        private void AbrirDibujo_Tapped(object sender, EventArgs e)
        {
            if (framedibujoPng.IsVisible)
            {
                framedibujoPng.IsVisible = false;
                if (framedibujo.IsVisible)
                {
                    firmita.Clear();
                }
                else
                {
                    framedibujo.IsVisible = true;
                }
            }
            else
            {
                if (framedibujo.IsVisible)
                {
                    firmita.Clear();
                }
                else
                {
                    framedibujo.IsVisible = true;
                }
            }
        }

        private void imgtapped_Tapped(object sender, EventArgs e)
        {

        }

        private void imgfirma_Tapped(object sender, EventArgs e)
        {

        }

        private async void btngraba_Clicked(object sender, EventArgs e)
        {
            if (!recorder.IsRecording)
            {
                seconds = 0; minutes = 0;
                isTimerRunning = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                    seconds++;
                    if (seconds.ToString().Length == 1) { lblseg.Text = "0" + seconds.ToString(); }
                    else { lblseg.Text = seconds.ToString(); }
                    if (seconds == 60)
                    {
                        minutes++;
                        seconds = 0;
                        if (minutes.ToString().Length == 1) { lblmin.Text = "0" + minutes.ToString(); }
                        else { lblmin.Text = minutes.ToString(); }
                        lblseg.Text = "00";
                    }
                    return isTimerRunning;
                });
                var audioRecordTask = await recorder.StartRecording();
                btngraba.IsEnabled = false;
                btngraba.BackgroundColor = Color.Silver;
                btnplay.IsEnabled = false;
                btnplay.BackgroundColor = Color.Silver;
                btndetener.IsEnabled = true;
                btndetener.BackgroundColor = Color.FromHex("#40E0D0");
                await audioRecordTask;
            }
        }

        private async void btndetener_Clicked(object sender, EventArgs e)
        {
            StopRecording();
            await recorder.StopRecording();
        }
        void StopRecording()
        {
            isTimerRunning = false;
            btngraba.IsEnabled = true;
            btngraba.BackgroundColor = Color.FromHex("#40E0D0");
            btnplay.IsEnabled = true;
            btnplay.BackgroundColor = Color.FromHex("#40E0D0");
            btndetener.IsEnabled = false;
            btndetener.BackgroundColor = Color.Silver;
            isTimer2Running = false;
            lblseg.Text = "00";
            lblmin.Text = "00";
        }
        private async void btnplay_Clicked(object sender, EventArgs e)
        {
            try
            {
                var stream = recorder.GetAudioFileStream();
                
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write)) { stream.CopyTo(fileStream); }
                
                if (fileName != null)
                {
                    StopRecording();
                    await CrossMediaManager.Current.Play(fileName);
                    seReprodujo = true;
                    //--TIMER DE REPRODUCCION
                    seconds = 0;
                    minutes = 0;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                        if (CrossMediaManager.Current.IsStopped()) { isTimer2Running = false; StopRecording(); }
                        seconds++;
                        if (seconds.ToString().Length == 1) { lblseg.Text = "0" + seconds.ToString(); }
                        else { lblseg.Text = seconds.ToString(); }
                        if (seconds == 60)
                        {
                            minutes++;
                            seconds = 0;
                            if (minutes.ToString().Length == 1) { lblmin.Text = "0" + minutes.ToString(); }
                            else { lblmin.Text = minutes.ToString(); }
                            lblseg.Text = "00";
                        }
                        return isTimer2Running;
                    });
                    //player.Play(fileName);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void graba_Tapped(object sender, EventArgs e)
        {
            if (!recorder.IsRecording)
            {
                seconds = 0; minutes = 0;
                isTimerRunning = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                    seconds++;
                    if (seconds.ToString().Length == 1) { lblseg.Text = "0" + seconds.ToString(); }
                    else { lblseg.Text = seconds.ToString(); }
                    if (seconds == 60)
                    {
                        minutes++;
                        seconds = 0;
                        if (minutes.ToString().Length == 1) { lblmin.Text = "0" + minutes.ToString(); }
                        else { lblmin.Text = minutes.ToString(); }
                        lblseg.Text = "00";
                    }
                    return isTimerRunning;
                });
                btndetener2.IsEnabled = true;
                var audioRecordTask = await recorder.StartRecording();
                btngraba.IsEnabled = false;
                btngraba.BackgroundColor = Color.Silver;
                btnplay.IsEnabled = false;
                btnplay.BackgroundColor = Color.Silver;
                btndetener.IsEnabled = true;
                btndetener.BackgroundColor = Color.FromHex("#40E0D0");
                await audioRecordTask;
            }
        }

        private async void pause_Tapped(object sender, EventArgs e)
        {
            StopRecording();
            await recorder.StopRecording();
        }

        private async void playy_Tapped(object sender, EventArgs e)
        {
            try
            {
                var stream = recorder.GetAudioFileStream();

                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write)) { stream.CopyTo(fileStream); }

                if (fileName != null)
                {
                    StopRecording();
                    await CrossMediaManager.Current.Play(fileName);
                    seReprodujo = true;
                    //--TIMER DE REPRODUCCION
                    seconds = 0;
                    minutes = 0;
                    btndetener2.IsEnabled = false;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                        if (CrossMediaManager.Current.IsStopped()) { isTimer2Running = false; StopRecording(); btndetener2.IsEnabled = false; }
                        seconds++;
                        if (seconds.ToString().Length == 1) { lblseg.Text = "0" + seconds.ToString(); }
                        else { lblseg.Text = seconds.ToString(); }
                        if (seconds == 60)
                        {
                            minutes++;
                            seconds = 0;
                            if (minutes.ToString().Length == 1) { lblmin.Text = "0" + minutes.ToString(); }
                            else { lblmin.Text = minutes.ToString(); }
                            lblseg.Text = "00";
                        }
                        return isTimer2Running;
                    });
                    //player.Play(fileName);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void savveNote_Tapped(object sender, EventArgs e)
        {
            string respuesta = "";
            string ftiyo = "";
            string ftiyo1 = "";
            string ftiyo2 = "";
            try
            {
                bool falloaudio = false;
                bool fallofirma = false;
                bool fallofoto = false;
                Byte[] bytes=null;
                Stream img=null;
                byte[] datafirma = null;
                try
                {
                    ftiyo = Convert.ToBase64String(imgbytes, 0, imgbytes.Length);
                }
                catch (Exception ex)
                {
                    fallofoto = true;
                }

                try
                {
                    var stream = recorder.GetAudioFileStream();
                    System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
                    bytes = br.ReadBytes((Int32)stream.Length);
                }
                catch (Exception ex)
                {
                    falloaudio = true;
                }
                try
                {
                    img = await firmita.GetImageStreamAsync(SignatureImageFormat.Png);
                    var memmo = (MemoryStream)img;//casteando a un memory
                    byte[] data = memmo.ToArray();//pasando a binarydata
                }
                catch (Exception ex)
                {
                    fallofirma = true;
                }
                

                if (fallofoto == true) { ftiyo = ""; fallofoto = true; } else { ftiyo = Convert.ToBase64String(imgbytes, 0, imgbytes.Length); }//FOTO
                if (falloaudio == true) { ftiyo1 = ""; falloaudio = true; } else { ftiyo1 = Convert.ToBase64String(bytes, 0, bytes.Length); }//AUDIO
                if (fallofirma == true) { ftiyo2 = ""; fallofirma = true; } else { ftiyo2 = Convert.ToBase64String(datafirma); }//FIRMA
                Notas2 notass = new Notas2()
                {
                    titulo = txttitle.Text,
                    contenido = txtcontenido.Text,
                    fecha = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    favorito = 0,
                    foto = ftiyo,
                    audio = ftiyo1,
                    firma = ftiyo2,
                    estado = "ACTIVO",
                    usuarioID = 0,
                    latitud = "",
                    longitud = "",
                    fecha_ULT_mod = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    email = email,
                    id = 0
                };
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    respuesta = await Controllers.AppController.setNotaUser(notass);
                }
                UserDialogs.Instance.HideLoading();
                if (respuesta == "ENVIADO")
                {
                    await DisplayAlert("INFO", "NOTA  GUARDADA", "OK");
                    await Navigation.PopAsync();
                    await Navigation.PopAsync();
                }
                else if (respuesta == "NO ENVIADO")
                {
                    await DisplayAlert("ALERTA", "NOTA NO GUARDADA", "OK");
                }
                else
                {
                    await DisplayAlert("ALERTA", "NOTA NO GUARDADA", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ALERTA", "NOTA NO GUARDADA", "OK");
            }
        }

        private void imgAudiotaped_Tapped(object sender, EventArgs e)
        {

        }
    }
}