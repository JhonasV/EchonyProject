using System.Linq;

namespace EchonyCore.Models
{
    public class ConnectionDao 
    {

    
       
        
        public ConnectionDao() { }
        public Usuario Verificacion(Usuario u)
        {
            
                Usuario detalles = null;
            using (var _context = new EchonyEntityContext())
            {
                detalles = _context.Usuario.Where(x => x.Email == u.Email && x.Clave == u.Clave).FirstOrDefault();
                if (detalles != null)
                {
                    if (detalles.Estado == "d")
                    {
                        detalles = null;
                    }



                }
            }
               
            return detalles;


        }

       public bool Registro(Usuario u)
        {
           
            // int rpta = 0;
            bool existe = false;
            Usuario detalles_email = null;
            Usuario detalles_nick = null;
            using (var _context = new EchonyEntityContext())
            {

          
                detalles_email = _context.Usuario.Where(x => x.Email == u.Email).FirstOrDefault();
                detalles_nick = _context.Usuario.Where(x => x.NickName == u.NickName).FirstOrDefault();
                if (detalles_email != null)
                {
                    // rpta = (int)Validacion_error.Email_en_uso;
                    existe = true;
                }
                else
                {

                    Correo cor = new Correo();
                    Usuario user = new Usuario
                    {
                        Nombre = u.Nombre,
                        Apellido = u.Apellido,
                        Email = u.Email,
                        Clave = u.Clave,
                        Estado = "d",
                        Codigo = new Correo().GetCodigo(8),
                        NickName = u.NickName,
                        ConfirmacionClave = u.ConfirmacionClave,


                    };



                    /*user.Nombre = u.Nombre;
                    user.Apellido = u.Apellido;
                    user.Email = u.Email;
                    user.Clave = u.Clave;
                    user.Estado = "d";
                    user.Codigo = new Correo().getCodigo(8);*/


            
                  cor.Destino = user.Email;
                    cor.Asunto = "Confirmacion de Correo";
                    cor.Remitente = "Echony";
                    string enlace = "http://localhost:51367/Login/EmailConfirm?code=" + user.Codigo;
                    cor.Mensaje = "Gracias por crear tu cuenta en Echony.\n Para completar el proceso de registro, haga click en el link debajo <br> <a href=" + enlace + ">Enlace de confirmacion</a>";
                    new Correo().PalomaMensajera(cor);


                _context.Usuario.Add(user);
                _context.SaveChanges();


                    Foto foto = new Foto
                    {
                        Id = user.Id,

                        RutaFoto = "ImagenDefault.png"
                        };

                    _context.Fotos.Add(foto);
                    _context.SaveChanges();
                }

            }
            
            return existe;
        }

        public bool ConfirmadorCuentas(string codigo)
        {
            
            bool actualizado = false;

            using (var _context = new EchonyEntityContext())
            {

           
                Usuario user = new Usuario();
                user = _context.Usuario.FirstOrDefault(x => x.Codigo == codigo);
                user.Estado = "a";
                 _context.SaveChanges();
                actualizado = true;
                //Codigo a probar
                /*Usuario actualizar = new Usuario();
                 actualizar.estado = "a";
                 db.Usuario.Attach(actualizar);
                 db.Entry(actualizar).Property(x => x.codigo == codigo ).IsModified = true;*/
            }
            return actualizado;
        }

    }
}