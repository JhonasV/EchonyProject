using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EchonyCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;

namespace EchonyCore.Controllers
{
    public class PerfilController : Controller
    {
     

    // GET: Perfil
    public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Usuario()
        {

             Usuario user = new Usuario();
             user.NickName = HttpContext.Request.Query["NickName"].ToString();
             PerfilDAO p = new PerfilDAO();
             Usuario u = p.GetUsuario(user);

             if (u != null)
             {
                 return View("Perfil", u);
             }
             else
             {
                 return View("Error");
             }
           

        }
        /*
       [HttpPost]
      public JsonResult Prueba(Posts post)
       {
           bool agregado = false;

           if(post == null)
           {
               return Json("Objeto nulo");
           }
           using (var bd = new EchonyEntityContext())
           {
               agregado = new PerfilDAO().CrearPost(post);

               return Json(agregado);
           }


       }*/

        /*public JsonResult MostrarPublicaciones(Posts p)
        {
            if(p == null)
            {
                return Json(false);
            }else
            {
                PerfilDAO c = new PerfilDAO();
                List<Posts> lista = c.ListarPosts(p);
                return Json(lista);
            }
        }*/
        [HttpPost]
        public IActionResult AddPublicacion(Publicaciones p)
        {
            if (p.UsuarioId == 0)
             {
                 return Json(false);
             }
             else
             {
                 PerfilDAO dao = new PerfilDAO();
              
                 dao.AddPublicacion(p);
                 return Json("Publicacion agregada");
             }
            
        }
        [HttpPost]
        public JsonResult GetPublicacion(Usuario ux)
        {
            if (ux == null)
             {
                 return Json(false);
             }
             else
             {


                 PerfilDAO dao = new PerfilDAO();
                 string lista = dao.GetPublicaciones(ux);
                 return Json(lista);
             }
            

        }

        public JsonResult AddComentario(int idUsuario, int idPublicacion, string comentario)
        {
            /* Comentarios com = new Comentarios
             {
                 UsuarioId = idUsuario,
                 PublicacionesId = idPublicacion,
                 Contenido_comentario = comentario,
                 Fecha_Publicacion = new DateTime()
             };
             if (com == null)
             {
                 return Json(false);
             }
             else
             {
                 PerfilDAO dao = new PerfilDAO();
                 dao.AddComentario(com);
                 return Json("Publicacion agregada");
             }*/
            return Json(false);
        }

        public IActionResult AddFotoPublicacion(int foto_id, IFormFile foto, string nick)
        {

            
            PerfilDAO dao = new PerfilDAO();
            if (!ModelState.IsValid)
            {
                return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = nick }));
            }
            //\wwwroot\images\Fotos_Usuarios\20180321212835.png
            var ruta = "wwwroot\\images\\Fotos_Usuarios\\" + foto.FileName;
            using(var stream = new FileStream(ruta, FileMode.Create))
            {

                foto.CopyTo(stream);
                dao.AddFotoPerfil(new Foto { Id = foto_id, Img = foto , RutaFoto = foto.FileName});
            }
            
            
            return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = nick }));

        }


        public ActionResult AgregarPublicacion(string Contenido, int UsuarioId, string NickName, IFormFile foto)
        {
            PerfilDAO dao = new PerfilDAO();
            DateTime fecha = DateTime.Now;


            var ruta = "";
            if(foto != null)
            {
                ruta = "wwwroot\\images\\Fotos_Usuarios\\" + foto.FileName;
                using (var stream = new FileStream(ruta, FileMode.Create))
                {

                    foto.CopyTo(stream);
                  
                    

                    bool exito = dao.AgregarPublicacion(new Publicaciones { Contenido = Contenido, UsuarioId = UsuarioId, Fecha = fecha, Foto = foto.FileName });
                }
            }
            else
            {
                bool exito = dao.AgregarPublicacion(new Publicaciones { Contenido = Contenido, UsuarioId = UsuarioId, Fecha = fecha });
            }
            
            
            
            //Perfil / Usuario ? NickName = JhonasV
           
           
            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
            //return Redirect("Perfil/Usuario?Nickname="+NickName);
        }
        [HttpGet]
        public ActionResult Busqueda(string r)
        {
            string nick = HttpContext.Session.GetString("nick");
            string cadena = r.Trim();
            PerfilDAO dao = new PerfilDAO();
            Usuario u = dao.GetUsuario(new Usuario { NickName = nick});
            List <Usuario> lista =  dao.Busqueda(cadena);
            ViewBag.data = lista;
            return View("Busqueda", lista);
        }
       
    }

}