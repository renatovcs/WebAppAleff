using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAppAleff.Application.Services;
using WebAppAleff.Application.Models;

namespace WebAppAleff.Controllers
{
    [AllowAnonymous]
    public class AcessoController : Controller
    {

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {

            AcessoModel.VerificaAdmin();

            ViewBag.ReturnUrl = returnUrl;
            return View();

        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var usuario = AcessoModel.Validar(login.Usuario, login.Senha);

            if (usuario != null)
            {

                FormsAuthentication.SetAuthCookie(usuario.Nome, login.LembrarMe);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("ListaAcessos", "Historico");
                }
            }
            else
            {
                ModelState.AddModelError("", "Nome de usuário incorreto ou senha incorreta. Tente novamente.");
                return View(login);
            }
        }

        public ActionResult Sobre()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login","Acesso");

        }

    }
}