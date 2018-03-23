using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EchonyCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EchonyCore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Informacion()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUsuario(Usuario u)
        {
           ConnectionDao con = new ConnectionDao();

            bool existe = con.Registro(u);
            if (existe)
            {
                u.Mensaje = "El correo eléctronico introducido ya está en uso";
                u.Email = "";
                u.Clave = "";
                u.ConfirmacionClave = "";
                return View("Registro", u);
                //return RedirectToAction("RegistroPage", u);
            }
            else
            {
                //Crear action MensajeRegistro y vista
                return View("Informacion");
            }
           
            
            

        }

        [HttpGet]
        public IActionResult EmailConfirm()
        {
           String codigo = HttpContext.Request.Query["code"].ToString();
             
            bool confirmacion = new ConnectionDao().ConfirmadorCuentas(codigo);
            if (confirmacion)
            {
                return View("Mensaje");
            }
            else
            {
                return View("Error");
            }
           
        }


        [HttpPost]
        public IActionResult Autorizacion(Usuario u)
        {
           ConnectionDao con = new ConnectionDao();
            Usuario detalles = con.Verificacion(u);
            if (detalles == null)
            {
                u.Mensaje = "Email y/o Contraseña inválido";
                u.Clave = "";
                return View("Index", u);
            }
            else
            {
                HttpContext.Session.SetString("nick", detalles.NickName);
                HttpContext.Session.SetInt32("id", detalles.Id);
                HttpContext.Session.SetString("email", detalles.Email);
               
                return RedirectToAction("Index", "Home");
            }


        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}