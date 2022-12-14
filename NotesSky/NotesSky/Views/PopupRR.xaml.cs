using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using NotesSky.Views;
using System.Collections.ObjectModel;
using NotesSky.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupRR : Rg.Plugins.Popup.Pages.PopupPage
    {
        public string id { get; set; }
        public string email { get; set; }
        public string numN { get; set; }
        public string estado { get; set; }
        public PopupRR(string id, string email, string estado)
        {
            InitializeComponent();

            this.email = email;


        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }







        private async void btnRR_Clicked(object sender, EventArgs e)
        {
            string respuesta = "";
            using (UserDialogs.Instance.Loading("ESPERE..."))
            {
                //respuesta = await Controllers.AppController.setRestoreRemindes(email);
            }
            UserDialogs.Instance.HideLoading();
        }
    }


}