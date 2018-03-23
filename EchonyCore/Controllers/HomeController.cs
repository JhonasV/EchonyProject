using EchonyCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchonyCore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public IActionResult Index()
        {
           Usuario u = new Usuario();
            u.NickName = HttpContext.Session.GetString("nick");
         if(u.NickName != null)
            {
                Usuario user = new PerfilDAO().GetUsuario(u);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
    }
}