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
    public IActionResult Index()
        {
            return View();
        }
        public IActionResult Perfil()
        {
            int idSesion = (int)HttpContext.Session.GetInt32("id");
            UsuarioViewModel us = new UsuarioViewModel();
            us.UsuarioSesion = new PerfilDAO().GetUsuarioById(new Usuario { Id =idSesion});
            us.UsuarioSecundario = new PerfilDAO().GetUsuarioById(new Usuario { Id = idSesion });
            us.publicaciones = new PerfilDAO().GetPublicacionesPrueba(new Models.Usuario { Id = idSesion });
            us.Amigos = new PerfilDAO().GetAmigos(new Usuario {Id = idSesion });
            return View("Perfil", us);
        }


        [HttpGet]
        public IActionResult Usuario(Usuario user)
        {
           
           
            UsuarioViewModel model = new UsuarioViewModel();
            PerfilDAO p = new PerfilDAO();


            int idSesion = (int)HttpContext.Session.GetInt32("id");
            string nickName = HttpContext.Session.GetString("nick");

            model.UsuarioSesion = p.GetUsuarioById(new Models.Usuario { Id = idSesion });
            model.UsuarioSecundario = p.GetUsuario(user);
            model.publicaciones = new PerfilDAO().GetPublicacionesPrueba(user);



            if (model.UsuarioSecundario != null)
            {
                model.EstadoAmistad = new PerfilDAO().GetAmigos(model.UsuarioSecundario.Id, idSesion);
            }


            if (model.UsuarioSecundario.Id.Equals(model.UsuarioSesion.Id))
            {
                return View("Perfil", model);
            }
            else
            {
                return View("PerfilSecundario", model);
            }
           
           
            
           /* if (model != null)
             {
                 return View("Perfil", model);
             }
             else
             {
                 return View("Error");
             }*/
           

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
      

     
        public JsonResult AddComment(Comentarios c)
        {
            c.Fecha_Publicacion = DateTime.Now;
            if (c.Contenido_comentario != null)
            {
                bool exito = new PerfilDAO().AddComentario(c);

                return Json(exito);
            }

            return Json(false);
            
    
        }

        public IActionResult AddFotoPublicacion(int foto_id, IFormFile foto, string nick)
        {

            
            PerfilDAO dao = new PerfilDAO();
            Usuario u = new Usuario();
            u = dao.GetUsuario(new Usuario { NickName = nick});
            if (!ModelState.IsValid)
            {
                return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = u.NickName, Id = u.Id}));
            }
            //\wwwroot\images\Fotos_Usuarios\20180321212835.png
            var ruta = "wwwroot\\images\\Fotos_Usuarios\\" + foto.FileName;
            using(var stream = new FileStream(ruta, FileMode.Create))
            {

                foto.CopyTo(stream);
                dao.AddFotoPerfil(new Foto { Id = foto_id, Img = foto , RutaFoto = foto.FileName});
            }
            
            
            return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = u.NickName, Id = u.Id }));

        }


        public JsonResult AgregarPublicacion(UsuarioViewModel model, IFormFile foto)
        {
            PerfilDAO dao = new PerfilDAO();
            DateTime fecha = DateTime.Now;
            bool exito = false;
            model.Publicacion.Fecha = fecha;
            var ruta = "";
            if(foto != null)
            {
                ruta = "wwwroot\\images\\Fotos_Usuarios\\" + foto.FileName;
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    foto.CopyTo(stream);                 
                     exito = dao.AgregarPublicacion(model.Publicacion);
                }
            }
            else if(model.Publicacion.Contenido != null)
            {     
                    exito = dao.AgregarPublicacion(model.Publicacion);
            }      
            return Json(exito);          
        }
        
        public PartialViewResult Publicaciones(int userId)
        {
            PerfilDAO dao = new PerfilDAO();
            int idSesion = (int)HttpContext.Session.GetInt32("id");
            PublicacionesViewModel model = new PublicacionesViewModel();
            model.ListaPublicaciones = dao.GetPublicaciones(userId); ;
            model.UsuarioSesion = dao.GetUsuarioById(new Usuario { Id = idSesion });
            model.Usuario = dao.GetUsuarioById(new Usuario { Id = userId });
            model.ListaComentarios = dao.GetAllComentarios();
            model.ListaReplies = dao.GetAllReplies();
            return PartialView(model);
        }

        public JsonResult AddReply(CommentReply cr)
        {
            PerfilDAO dao = new PerfilDAO();
            

            
            if (cr.Contenido_reply != null)
            {
                cr.Fecha = DateTime.Now;              
                return Json(dao.AddReply(cr));             
            }       
            return Json(false);
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
        public IActionResult EnviarSolicitud(SolicitudAmistad a, string NickName, int EmisorId, int ReceptorId)
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
        
       
        public IActionResult EliminarSolicitud(SolicitudAmistad a, string NickName)
        {
            new PerfilDAO().EliminarNotificacionAmistad(a);
            SolicitudAmistad soli = new SolicitudAmistad();
             soli.Estado = 5;
        
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

        public IActionResult Amigos()
        {
            PerfilDAO d = new PerfilDAO();
            UsuarioViewModel model = new UsuarioViewModel();

            int id = (int)HttpContext.Session.GetInt32("id");
            model.UsuarioSesion = new PerfilDAO().GetUsuarioById(new Usuario { Id = id });
           
          

            model.AmistadList = d.GetAmigos(new Receptor { UsuarioId = id });
            return View("Amigos", model);
        }
       
    }



}