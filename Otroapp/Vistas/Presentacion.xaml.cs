using Otroapp.Vistas.TutorialIntro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Otroapp.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Presentacion : ContentPage
    {
        public Presentacion()
        {
            InitializeComponent();
            Animacion();
        }
        public async Task Animacion()
        {
            logo.Opacity = 0;
            await logo.FadeTo(1, 2000);
            App.Current.MainPage = new Intro1();
        }
    }
}