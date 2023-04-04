using Firebase.Auth;
using Newtonsoft.Json;
using Otroapp.Modelo;
using Otroapp.VistasModelo;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Otroapp.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCorreo : ContentPage
    {
        public CrearCorreo()
        {
            InitializeComponent();
            CerrarSesion();
        }
        MediaFile file;
        string Idusuario;
        string rutafoto;
        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnCrearcuenta_Clicked(object sender, EventArgs e)
        {
            if (file != null)
            {
                if (!string.IsNullOrEmpty(txtnombre.Text))
                {
                    if (!string.IsNullOrEmpty(txtcorreo.Text))
                    {
                        if (!string.IsNullOrEmpty(txtcontraseña.Text))
                        {
                            await Crearcuenta();
                            await IniciarSesion();
                            await ObtenerIdusuario();
                            await SubirFotoStorage();
                            await InsertarNegocios();
                        }
                        else
                        {
                            await DisplayAlert("Alerta", "Agrege una contraseña", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Agrege un correo", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Alerta", "Agrege un Nombre", "OK");
                }

            }
            else
            {
                await DisplayAlert("Alerta", "Agrege una imagen", "OK");
            }
        }
        private void CerrarSesion()
        {
            Preferences.Remove("MyFirebaseRefreshToken");
        }
        private async Task Crearcuenta()
        {
            var funcion = new VMcrearcuenta();
            await funcion.Crearcuenta(txtcorreo.Text, txtcontraseña.Text);
        }
        private async Task IniciarSesion()
        {
            var funcion = new VMcrearcuenta();
            await funcion.ValidarCuenta(txtcorreo.Text, txtcontraseña.Text);
        }
        private async Task ObtenerIdusuario()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarContenido = await authProvider.RefreshAuthAsync(guardarId);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarContenido));
                Idusuario = guardarId.User.LocalId;
            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "Sesión Expirada", "OK");
            }

        }
        private async Task SubirFotoStorage()
        {
            var funcion = new VMnegocios();
            rutafoto = await funcion.SubirImagenStorage(file.GetStream(), Idusuario);
        }
        private async Task InsertarNegocios()
        {
            var funcion = new VMnegocios();
            var parametros = new Mnegocios();
            parametros.idusuario = Idusuario;
            parametros.idcategoria = "-";
            parametros.celular = "-";
            parametros.descripcion = "-";
            parametros.direccion = "-";
            parametros.foto = rutafoto; ;
            parametros.nombre = txtnombre.Text;
            parametros.idlocalidad = "-";
            parametros.prioridad = "0";
            await funcion.InsertarNegocios(parametros);
            await DisplayAlert("Listo", "Registrado", "OK");
        }
        private async void btnSubirfoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                Perfilfoto.Source = ImageSource.FromStream(file.GetStream);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}