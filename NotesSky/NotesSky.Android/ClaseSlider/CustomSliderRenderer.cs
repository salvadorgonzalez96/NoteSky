using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NotesSky.Droid.ClaseSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Slider), typeof(CustomSliderRenderer))]

namespace NotesSky.Droid.ClaseSlider
{
    public class CustomSliderRenderer : SliderRenderer
    {
        public CustomSliderRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                string colorSlider = "#1D619F";
                Control.ProgressDrawable.SetColorFilter(Xamarin.Forms.Color.FromHex(colorSlider).ToAndroid(), PorterDuff.Mode.SrcIn);

                // Set Progress bar Thumb color
                Control.Thumb.SetColorFilter(
                    Xamarin.Forms.Color.FromHex(colorSlider).ToAndroid(),
                    PorterDuff.Mode.SrcIn);
            }

        }
    }
}