
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class UsuarioViewModel
    {
        public Usuario UsuarioSesion { get; set; }
        public Usuario UsuarioSecundario { get; set; }
        public Publicaciones Publicacion { get; set; }
        public List<Usuario> lista { get; set; }
        public List<SolicitudAmistad> AmistadList { get; set; }
        public List<Publicaciones> publicaciones { get; set; }
        public SolicitudAmistad EstadoAmistad { get; set; }
        public List<SolicitudAmistad> Amigos { get; set; }
        //public IFormFile Foto { get; set; }
    }
}
