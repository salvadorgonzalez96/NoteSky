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

namespace NotesSky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public string email { get; set; }
        public MyPopupPage(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            //DisplayAlert("Stop","holaa","OK");
            Navigation.PopPopupAsync();
        }

        private void Closebtn_Clicked(object sender, EventArgs e)
        {
            
        }

        private void orderBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btnActualiza_Clicked(object sender, EventArgs e)
        {
            if (orderBy.SelectedItem != null)
            {
                String valorSelec = orderBy.SelectedItem as String;
                if (valorSelec.Contains("Fecha"))
                {
                    ObservableCollection<Notas> collection = new ObservableCollection<Notas>();
                    //----------test nada mas
                    try
                    {
                        if (await Controllers.AppController.getNotas(email) != null)
                        {
                            var items = await Controllers.AppController.getNotas(email);
                            if (items.Count >= 1)
                            {
                                var itemsOrder = items.OrderBy(a => a.fecha);
                                foreach (var item in itemsOrder)
                                {
                                    collection.Add(item);
                                }
                                PaginaSwipe.LimpiayActualiza(collection);
                            }
                        }
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                    }
                }
                else if (valorSelec.Contains("Titulo"))
                {
                    //var filto = noti.Where(a => a.titulo.ToLowerInvariant().Contains(currentSearchText)).ToList();
                    //ListaNotas.ItemsSource = filto;

                    ObservableCollection<Notas> collection = new ObservableCollection<Notas>();
                    //----------test nada mas
                    try
                    {
                        if (await Controllers.AppController.getNotas(email) != null)
                        {
                            var items = await Controllers.AppController.getNotas(email);
                            if (items.Count >= 1)
                            {
                                var itemsOrder = items.OrderBy(a => a.titulo);
                                foreach (var item in itemsOrder)
                                {
                                    collection.Add(item);
                                }
                                PaginaSwipe.LimpiayActualiza(collection);
                            }
                        }
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        await DisplayAlert("ERROR", "EL HOST NO RESPONDE VUELVELO A INTENTAR MAS TARDE!", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("ERROR", "OCURRIO UN ERROR DE NUESTRA PARTE!", "OK");
                    }
                }
            }
        }
    }
}