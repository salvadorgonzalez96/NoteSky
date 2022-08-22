using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NotesSky.Views;
using Xamarin.Essentials;
using NotesSky.Configuraciones;
using NotesSky.Splash;

[assembly: ExportFont("Madani Arabic Semi Bold.ttf", Alias = "Madani-SemiBold")]
[assembly: ExportFont("Madani Arabic Bold.ttf", Alias = "Madani-Bold")]
[assembly: ExportFont("Madani Arabic Medium.ttf", Alias = "Madani-Medium")]
[assembly: ExportFont("Madani Arabic Regular.ttf", Alias = "Madani-Regular")]
[assembly: ExportFont("Madani Arabic Medium.ttf", Alias = "Madani-Medium")]
[assembly: ExportFont("AcherusGrotesque-RegularItalic.otf", Alias = "AcherusGrotesque-RegularItalic")]
[assembly: ExportFont("AcherusGrotesque-Regular.otf", Alias = "AcherusGrotesque-Regular")]

namespace NotesSky
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "SwipeView_Experimental" });
            Device.SetFlags(new[] { "Expander_Experimental" });
            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new RecordatoriosVistaPrevia());

            //MainPage = new NavigationPage(new MainPage())
            //{
            //    BarBackgroundColor = Color.White,
            //    BarTextColor = Color.Black,
            //};

            MainPage = new NavigationPage(new SplashScreen())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black,
            };

            var theme = Preferences.Get("OSAppTheme", Enum.GetName(typeof(OSAppTheme), OSAppTheme.Unspecified));
            App.Current.UserAppTheme = (OSAppTheme)Enum.Parse(typeof(OSAppTheme), theme);

            // MainPage = new MainPage();
            //MainPage = new NavigationPage(new Configuracion());

            //MainPage = new PaginaSwipe();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
