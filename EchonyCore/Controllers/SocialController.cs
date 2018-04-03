using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EchonyCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EchonyCore.Controllers
{
    public class SocialController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult AceptarSolicitud(SolicitudAmistad a, int emisorId, int receptorId)
        {
            SocialDAO dao = new SocialDAO();
            dao.AceptarSolicitud(a, new Receptor { UsuarioId = receptorId}, new Emisor { UsuarioId = emisorId});
            return Json("");
        }

        public JsonResult ObtenerAmigos(Emisor em)
        {
            SocialDAO dao = new SocialDAO();
            dao.GetAmigos(em);
            return Json("");
        }
    }
}
