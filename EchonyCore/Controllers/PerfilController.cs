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
        public ActionResult Usuario(Usuario user)
        {

            /*Usuario user = new Usuario();
            user.NickName = HttpContext.Request.Query["NickName"].ToString();*/
            UsuarioViewModel model = new UsuarioViewModel();
            PerfilDAO p = new PerfilDAO();
            int idSesion = (int)HttpContext.Session.GetInt32("id");
            model.UsuarioSesion = p.GetUsuarioById(new Models.Usuario { Id = idSesion });
            model.UsuarioSecundario = p.GetUsuario(user);
            model.publicacionesDesc = new PerfilDAO().GetPubicacionesDesc(user);
            Usuario u = p.GetUsuario(user);
            ViewBag.Amigo = new PerfilDAO().GetAmigos(model.UsuarioSecundario.Id,idSesion);
           
            
            if (model != null)
             {
                 return View("Perfil", model);
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

        public JsonResult AddComentarioFuera(int idUsuario, int idPublicacion, string comentario)
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


        public ActionResult AddComentario(Comentarios c, string nick)
        {

            new PerfilDAO().AddComentario(c);

            return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = nick }));
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
           
           //return Redirect(Request.UrlReferrer.ToString());
            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
            //return Redirect("Perfil/Usuario?Nickname="+NickName);
        }
        [HttpGet]
        public ActionResult Busqueda(string r)
        {
           if(r == null || r== "")
            {

                return View("Error");
            }
            else
            {
                PerfilDAO dao = new PerfilDAO();
                UsuarioViewModel model = new UsuarioViewModel();
                PerfilDAO p = new PerfilDAO();
                int idSesion = (int)HttpContext.Session.GetInt32("id");
                model.UsuarioSesion = p.GetUsuarioById(new Models.Usuario { Id = idSesion });
                model.lista = dao.Busqueda(r);
                //List<Usuario> lista = dao.Busqueda(r);
                //ViewBag.data = lista;
                return View("Busqueda", model);
            }
          
        }
        [HttpPost]
        public ActionResult EnviarSolicitud(SolicitudAmistad a, string NickName, int EmisorId, int ReceptorId)
        {
            Emisor e = new Emisor
            {
                UsuarioId = EmisorId
            };

            Receptor r = new Receptor
            {
                UsuarioId = ReceptorId
            };

            PerfilDAO dao = new PerfilDAO();
            
            dao.NotificacionAmistad(a, e, r);
            
           
            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
        }
        
        [HttpPost]
        public ActionResult EliminarSolicitud(SolicitudAmistad a, string NickName)
        {
            new PerfilDAO().EliminarNotificacionAmistad(a);
            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
        }

        public IActionResult Notificaciones()
        {
            int idUsuario = (int)HttpContext.Session.GetInt32("id");
            UsuarioViewModel model = new UsuarioViewModel();
            model.UsuarioSesion = new PerfilDAO().GetUsuarioById(new Usuario { Id=idUsuario});
            SolicitudAmistad a = new SolicitudAmistad();
            Receptor r = new Receptor
            {
                Id = idUsuario
            };
            model.AmistadList = new PerfilDAO().GetNotificacionesAmistad(r);
            return View("Notificaciones", model);
        }
       
    }



}