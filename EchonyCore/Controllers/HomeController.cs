using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchonyCore.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUsuarioService _usuarioService;
        public HomeController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        // GET: Home
        public IActionResult Index()
        {
           Usuario u = new Usuario();
            u.NickName = HttpContext.Session.GetString("nick");
            //Usuario que estamos visitando
           
         if(u.NickName != null)
            {
                Usuario user = _usuarioService.GetUsuario(u);
                UsuarioViewModel model = new UsuarioViewModel();
                model.UsuarioSesion = user;
               
                return RedirectToAction("Perfil", "Perfil");
          }else
            {
                return RedirectToAction("Index", "Login");
            }            
        }
    }
}