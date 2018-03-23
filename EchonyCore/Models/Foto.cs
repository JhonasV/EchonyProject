using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EchonyCore.Models
{
    public class Foto
    {
        [ForeignKey("Usuario")]
        public int Id { get; set; }
        public string RutaFoto { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}