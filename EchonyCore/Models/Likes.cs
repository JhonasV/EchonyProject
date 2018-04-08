using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int? PublicacionesId { get; set; }
        public virtual Publicaciones Publicaciones { get; set; }
    }
}
