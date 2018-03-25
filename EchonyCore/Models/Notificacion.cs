using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class Notificacion
    {
        public int Id { get; set; }
        public int Usuario_Remite { get; set; }
        public int Usuario_Recibe { get; set; }
        public int Leido { get; set; }
        public String Tipo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
