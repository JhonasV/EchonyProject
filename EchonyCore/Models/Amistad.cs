using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class Amistad
    {
        public int Id { get; set; }
        public int Receptor { get; set; }
        public int Emisor { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
    }
}
