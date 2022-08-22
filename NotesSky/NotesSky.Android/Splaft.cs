using System;

using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using Acr.UserDialogs;
using System.IO;
using System.Timers;
using Felipecsl.GifImageViewLibrary;
using Android.Content;
using AndroidX.AppCompat.App;
using Android.App;
using Android.Gms.Common;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotesSky.Droid
{
    [Activity(Label = "NoteSky", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
    public class Splaft : AppCompatActivity
    {
        GifImageView gifImageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GifDesing);
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);

            Stream input = Resources.OpenRawResource(Resource.Drawable.giphy4);
            byte[] bytes = ConvertFileToByteArray(input);

            gifImageView.SetBytes(bytes);
            gifImageView.StartAnimation();

            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            IsPlayServicesAvailable();
            MessagingCenter.Unsubscribe<string>(this, "myService");
            MessagingCenter.Subscribe<string>(this, "myService", (value) =>
            {
                if (value == "1")
                {
                    StartService(new Intent(this, typeof(ServiceRemider)));
                }
                else
                {
                    StopService(new Intent(this, typeof(ServiceRemider)));
                }
            });
        }
        public void IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            bool isGooglePlayServce = resultCode != ConnectionResult.Success;
            Preferences.Set("isGooglePlayServce", isGooglePlayServce);

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //gifImageView.StopAnimation();
            //LoadApplication(new App());
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
        private byte[] ConvertFileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);

                }
                return ms.ToArray();
            }
        }
    }
}