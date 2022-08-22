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
    public partial class Page1 : ContentPage
    {
        public Command TouchCommand { get; }
        public Page1()
        {
            InitializeComponent();
            TouchCommand = new Command(()=>DisplayAlert("INFO","HOLA COMO ESTAS","OK"));
            BindingContext=this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            String texto = Config.Ecript.Encrypt(txtEncriptar.Text);
            txtresult.Text = texto;
        }

        private void btndes_Clicked(object sender, EventArgs e)
        {
            String texto = Config.Ecript.Decrypt(txtEncriptar.Text);
            txtresult.Text = texto;
        }
    }
}