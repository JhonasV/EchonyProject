using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class Publicaciones
    {
        public int Id { get; set; }

        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public string Foto { get; set; }


        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual List<Comentarios> Comentarios { get; set; }
      
        public virtual List<Likes> Like { get; set; }
    }
}
