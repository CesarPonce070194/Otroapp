using Otroapp.Vistas;
using Otroapp.Vistas.TutorialIntro;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Otroapp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CrearCorreo();
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
