using System;

using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using Acr.UserDialogs;
using System.IO;
using System.Timers;
using MediaManager;

namespace NotesSky.Droid
{
    [Android.App.Activity(Label = "NotesSky", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] Permission = {
         Android.Manifest.Permission.WriteExternalStorage,
         Android.Manifest.Permission.ReadExternalStorage,
         Manifest.Permission.RecordAudio,
         Manifest.Permission.AccessFineLocation,
         Manifest.Permission.Camera,
         Manifest.Permission.Internet
        };
        const int RequestId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);
            RequestPermissions(Permission, RequestId);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            CrossMediaManager.Current.Init(this);
            LoadApplication(new App());
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {

            }
            else
            {

            }
        }
    }
}