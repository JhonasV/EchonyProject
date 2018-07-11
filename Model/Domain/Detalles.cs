using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Detalles
    {
        [ForeignKey("Usuario")]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Sexo { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Pais { get; set; }
    }
}
