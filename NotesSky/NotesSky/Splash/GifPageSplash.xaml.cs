using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Splash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GifPageSplash : ContentPage
    {
        public GifPageSplash()
        {
            InitializeComponent();

           
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}