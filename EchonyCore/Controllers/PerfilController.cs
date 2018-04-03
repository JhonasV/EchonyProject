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
using Newtonsoft.Json;

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
            model.EstadoAmistad = new PerfilDAO().GetAmigos(model.UsuarioSecundario.Id,idSesion);
           
            
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
        
       
        public JsonResult EliminarSolicitud(SolicitudAmistad a)
        {
            new PerfilDAO().EliminarNotificacionAmistad(a);
            SolicitudAmistad soli = new SolicitudAmistad();
             soli.Estado = 5;
            string solicitud = JsonConvert.SerializeObject(soli);
            return Json(solicitud);
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

        public JsonResult NotificacionesJson(SolicitudAmistad a, string NickName, int EmisorId, int ReceptorId)
        {
            a.Fecha = DateTime.Now;

            Emisor e = new Emisor
            {
                UsuarioId = EmisorId
            };

            Receptor r = new Receptor
            {
                UsuarioId = ReceptorId
            };

            PerfilDAO dao = new PerfilDAO();
            Usuario usuarioReceptor = new Usuario();
            if (NickName != null || EmisorId != 0 || ReceptorId != 0)
            {
                usuarioReceptor = dao.GetUsuario(new Usuario { NickName = NickName });
                dao.NotificacionAmistad(a, e, r);
            }
            if(usuarioReceptor == null)
            {
                usuarioReceptor.Id = 0;
            }
            int idSesion = 0;
            if (HttpContext.Session.GetInt32("id") == null)
            {
                idSesion = 0;
            }
            else
            {
                idSesion = (int)HttpContext.Session.GetInt32("id");
            }
            
            SolicitudAmistad sol =  dao.GetAmigos(usuarioReceptor.Id, idSesion);
            if(sol == null)
            {
                SolicitudAmistad estado = new SolicitudAmistad();
                estado.Estado = 5;
                sol = estado;
            }
            string solicitud = JsonConvert.SerializeObject(sol);
            return Json(solicitud);
        }

        public ActionResult Amigos()
        {
            PerfilDAO d = new PerfilDAO();
            UsuarioViewModel model = new UsuarioViewModel();

            int id = (int)HttpContext.Session.GetInt32("id");
            model.UsuarioSesion = new PerfilDAO().GetUsuarioById(new Usuario { Id = id });
           
          

            model.AmistadList = d.GetAmigos(new Receptor { Id = id });
            return View("Amigos", model);
        }
       
    }



}