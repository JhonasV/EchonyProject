using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EchonyCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EchonyCore.Controllers
{
    public class SocialController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AceptarSolicitud(SolicitudAmistad a)
        {
            SocialDAO dao = new SocialDAO();
            dao.AceptarSolicitud(a);
            return Redirect(Url.Action("Notificaciones", "Perfil"));
           // return RedirectToAction("Notificaciones", "Perfil");
        }

        public IActionResult RechazarSolicitud(SolicitudAmistad a, string NickName)
        {
            new PerfilDAO().EliminarNotificacionAmistad(a);
            SolicitudAmistad soli = new SolicitudAmistad();
            soli.Estado = 5;

            return Redirect(Url.Action("Usuario", new Usuario { NickName = NickName }));
        }
        [HttpPost]
        public JsonResult ObtenerAmigos()
        {
            PerfilDAO dao = new PerfilDAO();
            if(HttpContext.Session.GetInt32("id") != null)
            {
                Receptor e = new Receptor()
                {
                    UsuarioId = (int)HttpContext.Session.GetInt32("id")
                };

                List<SolicitudAmistad> lista = dao.GetAmigos(e);
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
            SocialDAO dao = new SocialDAO();
            List<Likes> lista = dao.SetMeGusta(like);
            string json = JsonConvert.SerializeObject(lista);
            return Json(json);
        }

        [HttpPost]
        public JsonResult MegustaInfo(Likes like)
        {
            List<Likes> lista = new SocialDAO().getLikes(like);
           

            string json = JsonConvert.SerializeObject(lista);
            return Json(json);
        }
    }
}
