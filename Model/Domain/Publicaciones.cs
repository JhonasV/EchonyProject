using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Publicaciones
    {
        public int Id { get; set; }
       [Required(ErrorMessage = "Escribe algo que contar...")]
        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public string Foto { get; set; }


        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual List<Comentarios> Comentarios { get; set; }
      
        public virtual List<Likes> Like { get; set; }
    }
}
