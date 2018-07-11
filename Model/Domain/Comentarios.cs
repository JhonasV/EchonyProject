using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Comentarios
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int PublicacionesId { get; set; }
        public string Contenido_comentario { get; set; }
        public DateTime Fecha_Publicacion { get; set; }
        public string Foto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
