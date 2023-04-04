using Otroapp.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Firebase.Storage;
using System.IO;

namespace Otroapp.VistasModelo
{
    public class VMnegocios
    {
        string rutafoto;
        public async Task InsertarNegocios(Mnegocios parametros)
        {
            await Constantes.firebase
                .Child("Negocios")
                .PostAsync(new Mnegocios()
                {
                    celular = parametros.celular,
                    descripcion = parametros.descripcion,
                    direccion= parametros.direccion,
                    foto = parametros.foto,
                    idcategoria= parametros.idcategoria,
                    idlocalidad= parametros.idlocalidad,
                    idusuario= parametros.idusuario,
                    nombre= parametros.nombre,
                    prioridad= parametros.prioridad
                });
        }
        public async Task <string> SubirImagenStorage(Stream imageStrem, string Idusuario)
        {
            var Imagen = await new FirebaseStorage(Constantes.Storage)
                .Child("Negocios")
                .Child(Idusuario + ".jpg")
                .PutAsync(imageStrem);
            rutafoto = Imagen;
            return rutafoto;
        }
    }
}
