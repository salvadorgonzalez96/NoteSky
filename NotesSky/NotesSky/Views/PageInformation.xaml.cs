using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInformation : ContentPage
    {
        public PageInformation()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }
    }
}