using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Domain
{
    public class FotoViewModel
    {
        public string RutaFoto { get; set; }
        [Required(ErrorMessage = "Debe de seleccionar una foto.")]
      
        //public IFormFile Img { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
