using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class UsuarioViewModel
    {
        public Usuario UsuarioSesion { get; set; }
        public Usuario UsuarioSecundario { get; set; }
        public List<Usuario> lista { get; set; }
        public List<SolicitudAmistad> AmistadList { get; set; }
        public List<Publicaciones> publicacionesDesc { get; set; }
    }
}
