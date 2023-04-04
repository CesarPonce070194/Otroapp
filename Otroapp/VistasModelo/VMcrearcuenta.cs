using System;
using System.Collections.Generic;
using System.Text;
using Otroapp.VistasModelo;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Firebase.Auth;

namespace Otroapp.VistasModelo
{
    public class VMcrearcuenta
    {
        string Idusuario;
        public async Task Crearcuenta(string correo, string contraseña)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
            await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contraseña);

        }
        public async Task ValidarCuenta(string correo, string contraseña)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, contraseña);
                var serializartoken = JsonConvert.SerializeObject(auth);
                Preferences.Set("MyFirebaseRefreshToken", serializartoken);
                await App.Current.MainPage.DisplayAlert("Correcto", "Listo", "OK");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "OK");
            }
        }
    }
}
    