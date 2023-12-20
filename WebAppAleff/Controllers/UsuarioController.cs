using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAleff.Application.Services;
using WebAppAleff.Application.Models;

namespace WebAppAleff.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private const string _pwDefault = "pfrmdyc5bsaB!@_+";
        public ActionResult Index()
        {
            ViewBag.PwDefault = _pwDefault;
            return View(UsuarioModel.RecuperarLista());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int usuarioId)
        {
            return Json(UsuarioModel.RecuperarUsuario(usuarioId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel param)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = 0;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();
            }
            else
            {

                try
                {
                    if (param.UsuarioId > 0)
                    {
                        if (param.Senha == _pwDefault) param.Senha = "";

                        UsuarioModel.Alterar(param);

                        idSalvo = param.UsuarioId;
                    }
                    else
                    {
                        idSalvo = UsuarioModel.Incluir(param);
                    }

                }
                catch 
                {
                    resultado = "ERRO";
                }

            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int usuarioId)
        {
            var ret = false;

            ret = UsuarioModel.Excluir(usuarioId);

            return Json(ret);
        }
    }
}
