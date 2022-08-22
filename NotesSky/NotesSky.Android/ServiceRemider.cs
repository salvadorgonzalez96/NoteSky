using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NotesSky.Droid
{
    [Service(Label = "ServiceRemider")]
    public class ServiceRemider : Service
    {
        int counter = 0;
        bool isRunning = true;
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                MessagingCenter.Send<string>(counter.ToString(), "counterValue");
                counter++;
                return isRunning;
            });
            return StartCommandResult.Sticky;
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnDestroy()
        {
            StopSelf();
            counter =0;
            isRunning= false;
            base.OnDestroy();

        }
    }
}