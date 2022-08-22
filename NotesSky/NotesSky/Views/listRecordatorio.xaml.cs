using Acr.UserDialogs;
using NotesSky.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listRecordatorio : ContentPage
    {
        int counter { get; set; }
        public static CollectionView s;
        public static CollectionView cv { get; set; }//HACER ESTATICO EL COLLECTION VIEW
        public static ObservableCollection<Notas> noti { get; set; }
        public static string email { get; set; }
        public listRecordatorio(string email2)
        {
            InitializeComponent();
            s = ListaNotas;
            email = email2;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            counter = 0;
            string emailEntrada = "";
            try
            {
                emailEntrada = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Email");
            }
            catch
            {
                emailEntrada = "";
            }
            if (string.IsNullOrEmpty(emailEntrada))
            {
                await this.Navigation.PopModalAsync();
            }
            else
            {
                try
                {
                    getApiNotas(ListaNotas);
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                }
                cv = ListaNotas;//ENVIAR EL COLLECTION VIEW DEL DISEÑO A EL COLLECTION ESTATICO
            }
        }
        public async static void getApiNotas(CollectionView cc)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getRecordatorios(email) != null)
                    {
                        list = await Controllers.AppController.getRecordatorios(email);
                        if (list.Count >= 1)
                        {
                            ObservableCollection<Notas> collection = new ObservableCollection<Notas>(list);
                            cc.ItemsSource = null;
                            noti = collection;
                            cc.ItemsSource = noti;
                        }
                    }

                }
                UserDialogs.Instance.HideLoading();
            }
        }
        private async void ClickaNota_Tapped(object sender, EventArgs e)
        {
            StackLayout ab = sender as StackLayout;
            Frame aa = ab.Parent as Frame;
            //StackLayout ss = aa.Parent as StackLayout;
            Grid gg = ab.Children[0] as Grid;
            Label item = gg.Children[0] as Label;
            if (aa.Scale != 1)
            {
                //aa.BackgroundColor = new Color(63, 63, 63);
                await aa.ScaleTo(1, 300, Easing.SinIn);
                //aa.ScaleXTo(1, 300, Easing.SinIn);
            }
            else
            {
                //aa.BackgroundColor = new Color(127, 127, 127);
                await aa.ScaleTo(1.05, 300, Easing.SinIn);
                //aa.ScaleXTo(1.05, 300, Easing.SinIn);
                await Navigation.PushPopupAsync(new PopupClickNotas(item.Text, email,"1"));
            }
        }
        private void BuscadordeNotas_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentSearchText = e.NewTextValue.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(currentSearchText))
            {
                currentSearchText = string.Empty;
            }
            //var filto = noti.Where(a => a.titulo.StartsWith(e.NewTextValue));
            var filto = noti.Where(a => a.titulo.ToLowerInvariant().Contains(currentSearchText)).ToList();
            ListaNotas.ItemsSource = filto;
        }



        private async void Click_More(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyPopupPage(email));
        }

        private void ClickNew(object sender, EventArgs e)
        {

        }

        private async void clickAtras_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getRecordatorios(email) != null)
                    {
                        list = await Controllers.AppController.getRecordatorios(email);
                        if (list.Count >= 1)
                        {
                            ObservableCollection<Notas> collection = new ObservableCollection<Notas>(list);
                            ListaNotas.ItemsSource = null;
                            noti = collection;
                            ListaNotas.ItemsSource = noti;
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
                myrefresh.IsRefreshing = false;
            }
        }
    }
}