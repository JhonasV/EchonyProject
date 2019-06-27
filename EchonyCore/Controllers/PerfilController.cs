using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Model.Domain;
using Newtonsoft.Json;
using Service;

namespace EchonyCore.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPublicacionesService _publiService;
        private readonly IComentariosService _comentService;
        private readonly ICommentReplyService _replyService;
        private readonly IFotoService _fotoService;
        private readonly ISolicitudAmistadService _SolicService;
        private readonly INotificacionService _notiService;

        public PerfilController
            (IUsuarioService usuarioService, IPublicacionesService publiService, 
            IComentariosService comentariosService, ICommentReplyService replyService,
            IFotoService fotoService, ISolicitudAmistadService solicitudAmistad,
            INotificacionService notiService)
        {
            _usuarioService = usuarioService;
            _publiService = publiService;
            _comentService = comentariosService;
            _replyService = replyService;
            _fotoService = fotoService;
            _SolicService = solicitudAmistad;
            _notiService = notiService;
        }

    // GET: Perfil
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            int idSesion = (int)HttpContext.Session.GetInt32("id");
            UsuarioViewModel us = new UsuarioViewModel();
            us.UsuarioSesion =_usuarioService.GetUsuarioById(new Usuario { Id =idSesion});
            us.UsuarioSecundario = _usuarioService.GetUsuarioById(new Usuario { Id = idSesion });
            us.publicaciones = _publiService.GetPublicacionesPrueba(new Usuario { Id = idSesion });
            us.Amigos = _SolicService.GetAmigos(new Usuario {Id = idSesion });
            return View("Perfil", us);
        }

        [HttpGet]
        public IActionResult Usuario(Usuario user)
        {
            UsuarioViewModel model = new UsuarioViewModel();
          

            int idSesion = (int)HttpContext.Session.GetInt32("id");
           // string nickName = HttpContext.Session.GetString("nick");

            model.UsuarioSesion = _usuarioService.GetUsuarioById(new Usuario { Id = idSesion });
            model.UsuarioSecundario = _usuarioService.GetUsuario(user);
            model.publicaciones = _publiService.GetPublicacionesPrueba(user);

            if (model.UsuarioSecundario != null)
            {
                model.EstadoAmistad = _SolicService.GetAmigo(model.UsuarioSecundario.Id, idSesion);
            }

            if (model.UsuarioSecundario.Id.Equals(model.UsuarioSesion.Id))
            {
                return View("Perfil", model);
            }
            else
            {
                return View("PerfilSecundario", model);
            }
        }
    
        [HttpPost]
        public IActionResult AddPublicacion(Publicaciones p)
        {
            if (p.UsuarioId == 0)
             {
                 return Json(false);
             }
             else
            {                
                 _publiService.AddPublicacion(p);
                 return Json("Publicacion agregada");
             }
            
        }
      
        public JsonResult AddComment(Comentarios c)
        {
            c.Fecha_Publicacion = DateTime.Now;
            if (c.Contenido_comentario != null)
            {
                bool exito = _comentService.AddComentario(c);

                return Json(exito);
            }

            return Json(false);
            
    
        }
        [HttpPost]
        public JsonResult AddFotoPublicacion(IFormFile foto, int id)
        {  
            Usuario u = new Usuario();
            u = _usuarioService.GetUsuarioById(new Usuario { Id = id});
            if (!ModelState.IsValid)
            {
                return Json(false);
               // return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = u.NickName, Id = u.Id}));
            }
            //\wwwroot\images\Fotos_Usuarios\20180321212835.png
            var ruta = "wwwroot\\images\\Fotos_Usuarios\\" + foto.FileName;
            using(var stream = new FileStream(ruta, FileMode.Create))
            {
                Usuario user = new Usuario();
                user.Id = id;
                user.Avatar = foto.FileName;
                
                foto.CopyTo(stream);
                _fotoService.AddFotoPerfil(user);
                //_fotoService.AddFotoPerfil(new Foto { Id = foto_id, RutaFoto = foto.FileName});
                //_fotoService.AddFotoPerfil(new Foto { Id = foto_id, Img = foto , RutaFoto = foto.FileName});
            }
            return Json(foto.FileName);
            //return Redirect(Url.Action("Usuario", "Perfil", new Usuario { NickName = u.NickName, Id = u.Id }));
        }
        [HttpPost]
        public IActionResult AgregarPublicacion([FromBody] UsuarioViewModel model, [FromBody]  IFormFile foto, [FromBody]  Publicaciones publi)
        {
            string photoName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg";
            DateTime fecha = DateTime.Now;
            bool exito = false;
            model.Publicacion.Fecha = fecha;
            var ruta = "";
            if(foto != null)
            {
                ruta = $"wwwroot/images/{photoName}";
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    foto.CopyTo(stream);
                    string rutaBd = $"/images/{photoName}";
                    model.Publicacion.Foto = rutaBd;
                    exito = _publiService.AgregarPublicacion(model.Publicacion);
                }
            }
            else if(model.Publicacion.Contenido != null)
            {     
                    exito = _publiService.AgregarPublicacion(model.Publicacion);
            }      
            return Ok(exito);          
        }

        [HttpPost]
        public IActionResult AddPublication([FromBody] UsuarioViewModel model, [FromBody]  IFormFile foto, [FromBody]  Publicaciones publi)
        {
            string photoName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg";
            DateTime fecha = DateTime.Now;
            bool exito = false;
            model.Publicacion.Fecha = fecha;
            var ruta = "";
            if (foto != null)
            {
                ruta = $"wwwroot/images/{photoName}";
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    foto.CopyTo(stream);
                    string rutaBd = $"/images/{photoName}";
                    model.Publicacion.Foto = rutaBd;
                    exito = _publiService.AgregarPublicacion(model.Publicacion);
                }
            }
            else if (model.Publicacion.Contenido != null)
            {
                exito = _publiService.AgregarPublicacion(model.Publicacion);
            }
            return Ok(exito);
        }

        public PartialViewResult Publicaciones(int userId)
        {
            
            int idSesion = (int)HttpContext.Session.GetInt32("id");
            PublicacionesViewModel model = new PublicacionesViewModel();
            model.ListaPublicaciones = _publiService.GetPublicaciones(userId); ;
            model.UsuarioSesion = _usuarioService.GetUsuarioById(new Usuario { Id = idSesion });
            model.Usuario = _usuarioService.GetUsuarioById(new Usuario { Id = userId });
            model.ListaComentarios = _comentService.GetAllComentarios();
            model.ListaReplies = _replyService.GetAllReplies();
            return PartialView(model);
        }

        public IActionResult GetAllPosts()
        {
            var posts = _publiService.GetPublicaciones();
            return Ok(posts);
        }

        /*Usar un PublicacionesViewModel y testear lo que llega por parametro*/
        public JsonResult AddReply(PublicacionesViewModel model)
        {
           
            if (model.CommentReply.Contenido_reply != null)
            {
                             
                return Json(_replyService.AddReply(model.CommentReply));             
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
                UsuarioViewModel model = new UsuarioViewModel();               
                int idSesion = (int)HttpContext.Session.GetInt32("id");
                model.UsuarioSesion = _usuarioService.GetUsuarioById(new Usuario { Id = idSesion });
                model.lista = _usuarioService.Busqueda(r);
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
            
            _notiService.NotificacionAmistad(a, e, r);
           
            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
        }
        
        public IActionResult EliminarSolicitud(SolicitudAmistad a, string NickName)
        {
            _notiService.EliminarNotificacionAmistad(a);
            SolicitudAmistad soli = new SolicitudAmistad();
             soli.Estado = 5;
        
            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
        }

        public IActionResult Notificaciones()
        {
            int idUsuario = (int)HttpContext.Session.GetInt32("id");
            UsuarioViewModel model = new UsuarioViewModel();
            model.UsuarioSesion = _usuarioService.GetUsuarioById(new Usuario { Id=idUsuario});
            SolicitudAmistad a = new SolicitudAmistad();
            Receptor r = new Receptor
            {
                Id = idUsuario
            };
            model.AmistadList = _SolicService.GetNotificacionesAmistad(r);
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

            
            Usuario usuarioReceptor = new Usuario();
            if (NickName != null || EmisorId != 0 || ReceptorId != 0)
            {
                usuarioReceptor = _usuarioService.GetUsuario(new Usuario { NickName = NickName });
                _notiService.NotificacionAmistad(a, e, r);
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
            
            SolicitudAmistad sol =  _SolicService.GetAmigo(usuarioReceptor.Id, idSesion);
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
           
            UsuarioViewModel model = new UsuarioViewModel();

            int id = (int)HttpContext.Session.GetInt32("id");
            model.UsuarioSesion = _usuarioService.GetUsuarioById(new Usuario { Id = id });
           
          

            model.AmistadList = _SolicService.GetAmigos(new Receptor { UsuarioId = id });
            return View("Amigos", model);
        }
       
    }



}