using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Service;

namespace EchonyCore.Controllers
{
    public class LoginController : Controller {

        private readonly IAccountService _accountService;
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
           

            bool existe = _accountService.Registro(u);
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

            bool confirmacion = _accountService.ConfirmadorCuentas(codigo);
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
        public IActionResult Autorizacion(UsuarioViewModel user)
        {
            
            Usuario u = new Usuario()
            {
                Email = user.UsuarioSesion.Email,
                Clave = user.UsuarioSesion.Clave
            };
          
            Usuario detalles =_accountService.Verificacion(u);
            if (detalles == null)
            {
                u.Mensaje = "Email y/o Contraseña inválido";
                u.Clave = "";
                UsuarioViewModel model = new UsuarioViewModel();
                model.UsuarioSesion = u;
                return View("Index", model);
               
            }
            else
            {
                HttpContext.Session.SetString("nick", detalles.NickName);
                HttpContext.Session.SetInt32("id", detalles.Id);
                HttpContext.Session.SetString("email", detalles.Email);
                
               
                return RedirectToAction("Perfil", "Perfil");
            }


        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}