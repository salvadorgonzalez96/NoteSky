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
    public partial class papelera : ContentPage
    {
        public string email { get; set; }
        int counter { get; set; }
        public static CollectionView s;
        public static CollectionView cv { get; set; }//HACER ESTATICO EL COLLECTION VIEW
        public static ObservableCollection<Notas> noti { get; set; }



        public static string numN { get; set; }
        public static string estado { get; set; }







        public papelera(string email, string numN, string estado)
        {
            InitializeComponent();
            this.email = email;
            numN = numN;
            estado = estado;

            s = ListaNotas;
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
                    getApiNotas(ListaNotas, email);
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
        public async static void getApiNotas(CollectionView cc, string email)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getPapelera(email) != null)
                    {
                        list = await Controllers.AppController.getPapelera(email);
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
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyPopupPage(email));
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

        private async void clickanota_Tapped(object sender, EventArgs e)
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
                //await Navigation.PushPopupAsync(new PopupClickNotas(item.Text, email, "ACTIVO"));
                await Navigation.PushPopupAsync(new PopupClickPapelera(item.Text, email, "ACTIVO"));
                
            }
        }
        
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }

        private async void myrefresh_Refreshing(object sender, EventArgs e)
        {

        }


        public async void getPapelera(CollectionView cc)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getPapelera(email) != null)
                    {
                        list = await Controllers.AppController.getPapelera(email);
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

        private async void backbutton_Tapped(object sender, EventArgs e)
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
                    if (await Controllers.AppController.getPapelera(email) != null)
                    {
                        list = await Controllers.AppController.getPapelera(email);
                        if (list.Count >= 1)
                        {
                            ObservableCollection<Notas> collection = new ObservableCollection<Notas>(list);
                            ListaNotas.ItemsSource = null;
                            noti = collection;
                            ListaNotas.ItemsSource = noti;
                        }
                        else
                        {
                            ListaNotas.ItemsSource = null;
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
                myrefresh.IsRefreshing = false;
            }
        }
    }
}