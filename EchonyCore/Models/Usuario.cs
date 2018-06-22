using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EchonyCore.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Correo es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un Correo valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "La Contraseña minimo 5 caracteres")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El Nombre debe de contener minimo 5 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El Apellido debe de contener minimo 5 caracteres")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El Nombre de Usuario debe de contener minimo 5 caracteres")]
        public string NickName { get; set; }
        public string Mensaje { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La Confirmacion de la Contraseña es obligatoria")]
        [Compare("Clave", ErrorMessage = "Las Contraseñas no coinciden")]
        public string ConfirmacionClave { get; set; }

        public virtual Foto Foto { get; set; }

        public List<Publicaciones> Publicaciones { get; set; }

        public List<Comentarios> Comentarios { get; set; }
        
        public List<SolicitudAmistad> Amigos { get; set; }

        public int Privada { get; set; }

        public virtual Detalles Detalles { get; set; }

        public virtual List<Likes> Likes { get; set; }

    }
}