using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using System.Windows.Input;
using NotesSky.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using NotesSky.Configuraciones;
using NotesSky.Views;
using Acr.UserDialogs;

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class PaginaSwipe : ContentPage
    {
        public static Command LongPress { get; set; }
        int counter { get; set; }
        public bool abiertoMenu = true;
        public static CollectionView s;
        public static ObservableCollection<Notas> noti { get; set; }
        public static CollectionView cv { get; set; }//HACER ESTATICO EL COLLECTION VIEW
        //public static ObservableCollection<Notas> notiOrder  { get; set; }
        public static string email { get; set; }
        public string pass { get; set; }
        public string nombre { get; set; }
        public string id { get; set; }
        public PaginaSwipe(string email2,string pass,string nom,string id)
        {
            InitializeComponent();
            email = email2;
            this.pass = pass;
            this.nombre= nom;
            this.id = id;
            txtbienvenido.Text = "Hola " + nombre + ","+ obtenerbienvenida();
            MyMenu = GetMenus();
            s = ListaNotas;
            //notiOrder = new ObservableCollection<Notas>();
            //ListaNotas.ItemsSource = noti;
            LongPress = new Command(() => DisplayAlert("INFO", "HOLA COMO ESTAS", "OK"));
            this.BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            counter = 0;
            string emailEntrada = "";
            try
            {
                emailEntrada = await Xamarin.Essentials.SecureStorage.GetAsync("AppRN_Email");
                email = emailEntrada;
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
                LongPress = new Command(() => DisplayAlert("INFO", "HOLA COMO ESTAS", "OK"));
            }
        }
        public string obtenerbienvenida() 
        {
            string msg = "";
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 12)
            {
                msg = "Buenos Dias";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 16)
            {
                msg = "Buenas Tardes";
            }
            else if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour <= 21)
            {
                msg = "Buenas Tardes Noches";
            }
            else if (DateTime.Now.Hour >= 21 && DateTime.Now.Hour <= 24)
            {
                msg = "Buenas Noches";
            }
            return msg;
        }
        protected override bool OnBackButtonPressed()
        {
            bool res=true;
            //ACCION DE PREGUNTAR CERRAR SESION
            Device.InvokeOnMainThreadAsync(async()=> 
            {
                res = await this.DisplayAlert("ALERTA!","DESEA CERRAR SESION","SI","NO");
                if (res) {
                    bool persistenciafallo = false;
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Email", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Nom", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Ids", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Pass", "");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    try
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("AppRN_Login", "false");
                    }
                    catch
                    {
                        persistenciafallo = true;
                    }
                    if (!persistenciafallo)
                    {
                        //await this.Navigation.PopModalAsync();
                        await Navigation.PushModalAsync(new PantallaLogin());
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "PORFAVOR. Intentelo de nuevo!. \nOcurrio un Error de nuestro lado", "OK");
                    } 
                }
            });
            //return base.OnBackButtonPressed();
            return res;
        }
        public async static void getApiNotas(CollectionView cc)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getNotas(email) != null)
                    {
                        list = await Controllers.AppController.getNotas(email);
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
        public static void LimpiayActualiza(ObservableCollection<Notas> cc) 
        {
            cv.ItemsSource = null;
            noti = cc;
            cv.ItemsSource = noti;
        }
        //public static ObservableCollection<Notas> LlenarNotasTest() 
        //{
        //    return new ObservableCollection<Notas>
        //    {
        //        new Notas
        //        {
        //            id=1,
        //            titulo= "NOTA2",
        //            contenido="hola como estas",
        //            fecha= new DateTime(),
        //            foto="foto",
        //            audio="audio",
        //            firma="firma",
        //            favorito= 1,
        //            estado="ACTIVO",
        //            usuarioID= 1,
        //            latitud="16.1",
        //            longitud="-15.8",
        //            fecha_ULT_mod= new DateTime()
        //        },
        //        new Notas
        //        {
        //            id=2,
        //            titulo= "NOTA1",
        //            contenido="aki bien y tu",
        //            fecha= new DateTime(),
        //            audio="audio",
        //            firma="firma",
        //            favorito= 1,
        //            estado="ACTIVO",
        //            usuarioID= 1,
        //            latitud="16.1",
        //            longitud="-15.8",
        //            fecha_ULT_mod= new DateTime()
        //        },
        //        new Notas
        //        {
        //            id=3,
        //            titulo= "NOTA3",
        //            contenido="jejeje",
        //            fecha= new DateTime(),
        //            foto="foto",
        //            audio="audio",
        //            firma="firma",
        //            favorito= 1,
        //            estado="ACTIVO",
        //            usuarioID= 1,
        //            latitud="16.1",
        //            longitud="-15.8",
        //            fecha_ULT_mod= new DateTime()
        //        }
        //    };
        //}
        //public void VuelveALlenar()
        //{
        //    ListaNotas.ItemsSource = null;
        //    ListaNotas.ItemsSource = notiOrder;
        //}
        private void OpenSwipe(object sender, EventArgs e)
        {
            btnmenuc.Focus();
            if(counter == 0) { abiertoMenu = false; counter++; } else { counter++; }
            if (abiertoMenu)
            {
                abiertoMenu = false;
                Swipemenu.Close();
                CloseAnimation();
            }
            else
            {
                abiertoMenu = true;
                OpenAnimation();
                Swipemenu.Open(OpenSwipeItem.LeftItems);
            }
        }

        private void CloseSwipe(object sender, EventArgs e)
        {
            MenuStacklayout.Focus();
            if (counter == 0) { abiertoMenu = false; counter++; } else { counter++; }
            if (abiertoMenu)
            {
                abiertoMenu = false;
                Swipemenu.Close();
                CloseAnimation();
            }
            else
            {
                abiertoMenu = true;
                OpenAnimation();
                Swipemenu.Open(OpenSwipeItem.LeftItems);
            }
        }
        public List<Menu> MyMenu { get; set; }
        private List<Menu> GetMenus()
        {
            return new List<Menu>
            {
                new Menu{ name = "INICIO" ,Icon="home.png"},
                new Menu{ name = "PERFIL" ,Icon="user_male_26px.png"},
                new Menu{ name = "RECORDATORIOS" ,Icon="notification_26px.png"},
                new Menu{ name = "NOTAS" ,Icon="page_26px.png"},
                new Menu{ name = "FAVORITOS" ,Icon="favorite.png"},
                new Menu{ name = "PAPELERA" ,Icon="papelera.png"},
                new Menu{ name = "ACERCA DE.." ,Icon="about1.png"},
                new Menu{ name = "CONFIGURACION" ,Icon="config.png"}
            };
        }
        private async void OpenAnimation()
        {
            await Task.WhenAll(
                SwipeContent.ScaleYTo(0.9, 300, Easing.SinOut), 
                SwipeContent.RotateTo(-15, 300, Easing.SinOut)
                );
            pancake.CornerRadius = 20;
        }
        private async void CloseAnimation()
        {
            await Task.WhenAll(
                SwipeContent.ScaleYTo(1, 300, Easing.SinOut),
                SwipeContent.RotateTo(0, 300, Easing.SinOut)
                );
            pancake.CornerRadius = 0;
        }

        private void Swipemenu_SwipeStarted_1(object sender, SwipeStartedEventArgs e)
        {
            if (abiertoMenu)
            {
                abiertoMenu = false;
                Swipemenu.Close();
                CloseAnimation();
            }
            else
            {
                abiertoMenu = true;
                OpenAnimation();
                Swipemenu.Open(OpenSwipeItem.LeftItems);
            }
        }

        private void Swipemenu_SwipeEnded_1(object sender, SwipeEndedEventArgs e)
        {
            if (!e.IsOpen)
            {
                if (abiertoMenu)
                {
                    abiertoMenu = false;
                    Swipemenu.Close();
                    CloseAnimation();
                }
                else
                {
                    abiertoMenu = true;
                    OpenAnimation();
                    Swipemenu.Open(OpenSwipeItem.LeftItems);
                }
            }
            else
            {
                if (abiertoMenu)
                {
                    abiertoMenu = false;
                    Swipemenu.Close();
                    CloseAnimation();
                }
                else
                {
                    abiertoMenu = true;
                    OpenAnimation();
                    Swipemenu.Open(OpenSwipeItem.LeftItems);
                }
            }
        }

        private void Swipemenu_SwipeChanging(object sender, SwipeChangingEventArgs e)
        {
            //if (e.SwipeDirection == SwipeDirection.Right)
            //{
            //    CloseAnimation();
            //}
            //else if (e.SwipeDirection == SwipeDirection.Left)
            //{
            //    OpenAnimation();
            //}
        }

        private void Swipemenu_CloseRequested(object sender, CloseRequestedEventArgs e)
        {
            OpenAnimation();
        }

        private async void Click_User(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("ESPERE....");
            await Navigation.PushModalAsync(new Views.UpdateData(id,email));
        }

        private async void Click_More(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyPopupPage(email));
        }

        private async void ClickNew(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Deseas crear?: ", "Cancel", null, "Nota", "Recordatorio");
            if (action == "Nota")
            {
                NavigationPage nv = new NavigationPage(new NotasNew(id, email));
                nv.SetAppThemeColor(NavigationPage.BarBackgroundColorProperty, Color.White, Color.FromHex("#2E312E"));
                await Navigation.PushModalAsync(nv);
            }
            else if (action=="Recordatorio")
            {
                //await Navigation.PushModalAsync(new RecodatoriosNew(id, email));
            }
            else if(action=="Cancel" || action == null)
            {

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
                await Navigation.PushPopupAsync(new PopupClickNotas(item.Text,email, "INACTIVO"));
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

        private async void MenuClicked(object sender, EventArgs e)
        {
            //var labels = MenuStacklayout.Children.Where(x => x is Label).ToList();
            //var StackLayouts = MenuStacklayout.Children.Where(x => x is StackLayout).ToList();
            //StackLayout ee = StackLayouts[0] as StackLayout;
            //Label item = ee.Children[1] as Label;
            //String contenido = item.Text;
            if (abiertoMenu) { abiertoMenu = false; Swipemenu.Close(); CloseAnimation(); }
            else { abiertoMenu = true; OpenAnimation(); Swipemenu.Open(OpenSwipeItem.LeftItems); }

            StackLayout ss = sender as StackLayout;//OBTENER EL ITEM DE SWIPE
            Label ll = ss.Children[1] as Label;
            String contenido = ll.Text;
            if (contenido == "CONFIGURACION")
            {
                await Navigation.PushModalAsync(new Configuracion(id));
            }
            else if(contenido== "PERFIL") 
            {
                UserDialogs.Instance.ShowLoading("ESPERE....");
                await Navigation.PushModalAsync(new UpdateData(id,email));
            }
            else if (contenido == "RECORDATORIOS")
            {
                await Navigation.PushModalAsync(new listRecordatorio(email));
            }
            else if (contenido == "NOTAS")
            {
                await Navigation.PushModalAsync(new listnota(email));
            }
            else if (contenido == "FAVORITOS")
            {
                await Navigation.PushModalAsync(new Fav(email,"",""));
            }
            else if (contenido == "PAPELERA")
            {
                await Navigation.PushModalAsync(new papelera(email, "", ""));
            }
            else if (contenido == "ACERCA DE..")
            {
                await Navigation.PushModalAsync(new PageInformation());
            }

        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (UserDialogs.Instance.Loading("ESPERE..."))
                {
                    List<Notas> list = new List<Notas>();
                    if (await Controllers.AppController.getNotas(email) != null)
                    {
                        list = await Controllers.AppController.getNotas(email);
                        if (list.Count >= 1)
                        {
                            ObservableCollection<Notas> collection = new ObservableCollection<Notas>(list);
                            ListaNotas.ItemsSource = null;
                            noti = collection;
                            ListaNotas.ItemsSource = noti;
                        }
                        else { ListaNotas.ItemsSource = null; }
                    }
                }
                UserDialogs.Instance.HideLoading();
                myrefresh.IsRefreshing = false;
            }
        }
        //private void Swipemenu_CloseRequested(object sender, CloseRequestedEventArgs e)
        //{
        //    CloseAnimation();
        //}
    }
    public class Menu
    {
        public string name { get; set; }
        public string Icon { get; set; }
    }

}