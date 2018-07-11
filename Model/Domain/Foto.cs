using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Model.Domain
{
    public class Foto
    {
        [ForeignKey("Usuario")]
        public int Id { get; set; }
        public string RutaFoto { get; set; }
        [Required(ErrorMessage = "Debe de seleccionar una foto.")]
       /* [NotMapped]
        public IFormFile Img { get; set; }*/
        public virtual Usuario Usuario { get; set; }

    }
}