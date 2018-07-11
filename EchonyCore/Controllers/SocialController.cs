using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service;

namespace EchonyCore.Controllers
{
    public class SocialController : Controller
    {

        private readonly ISolicitudAmistadService _solicitud;
        private readonly INotificacionService _notiService;
        private readonly ILikesService _likeService;
        public SocialController
            (ISolicitudAmistadService solicitud,
            INotificacionService notiService,
            ILikesService likesService)
        {
            _solicitud = solicitud;
            _notiService = notiService;
            _likeService = likesService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AceptarSolicitud(SolicitudAmistad a)
        {
            
            _solicitud.AceptarSolicitud(a);
            return Redirect(Url.Action("Notificaciones", "Perfil"));
           // return RedirectToAction("Notificaciones", "Perfil");
        }

        public IActionResult RechazarSolicitud(SolicitudAmistad a, string NickName)
        {
            _notiService.EliminarNotificacionAmistad(a);
            SolicitudAmistad soli = new SolicitudAmistad();
            soli.Estado = 5;

            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
        }

        [HttpPost]
        public JsonResult ObtenerAmigos()
        {
            if(HttpContext.Session.GetInt32("id") != null)
            {
                Receptor e = new Receptor()
                {
                    UsuarioId = (int)HttpContext.Session.GetInt32("id")
                };

                List<SolicitudAmistad> lista = _solicitud.GetAmigos(e);
                return Json(lista);
            }
            else
            {
                return Json("Error en obtener los Amigos");
            }
            
            
            
        }

        [HttpPost]
        public JsonResult MeGusta(Likes like)
        {
            
            List<Likes> lista = _likeService.SetMeGusta(like);
            string json = JsonConvert.SerializeObject(lista);
            return Json(json);
        }

        [HttpPost]
        public JsonResult MegustaInfo(Likes like)
        {
            List<Likes> lista = _likeService.getLikes(like);
           

            string json = JsonConvert.SerializeObject(lista);
            return Json(json);
        }
    }
}
