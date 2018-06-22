using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class CommentReply
    {
        public int Id { get; set; }
        public string Contenido_reply { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int PublicacionesId { get; set; }
        public int ComentariosId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Publicaciones Publicaciones { get; set; }
    }
}
