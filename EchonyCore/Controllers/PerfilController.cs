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
        public JsonResult AddPublicacion(Publicaciones p)
        {
            if (p == null)
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

        /*public IActionResult AddFotoPublicacion(int foto_id, IFormFile foto)
        {

            if (foto != null)
            {
                string ruta = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(foto.FileName);
               
                foto.SaveAs(System.IO.Path.Combine("~/Fotos_Usuarios/", ruta));

                PerfilDAO dao = new PerfilDAO();
                string mensaje = dao.AddFotoPerfil(new Foto { Id = foto_id, RutaFoto = ruta });
                return RedirectToAction("Usuario");
            }

            return RedirectToAction("Usuario");




        }*/
    }
}