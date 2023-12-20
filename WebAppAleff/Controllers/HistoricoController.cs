using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAleff.Application.Dto;
using WebAppAleff.Domain.Entities;
using WebAppAleff.Application.Models;

namespace WebAppAleff.Controllers
{
    [Authorize]
    public class HistoricoController : Controller
    {
        // GET: Historico
        public ActionResult ListaAcessos()
        {
            var lista = HistoricoModel.RecurarListaAcessos();

            var listaUsuariosBanco = HistoricoModel.RecurarListaUsuairosAcesso();

            var listaUsuarios = new List<Usuario>
            {
                new Usuario(0, "Todos")
            };

            foreach (Usuario usuario in listaUsuariosBanco)
                listaUsuarios.Add(usuario);

            ViewBag.ListaUsuarios = new SelectList(listaUsuarios, "UsuarioId", "Nome" );

            var listaAcessosHora = HistoricoModel.AcessosHora();

            var horas = new List<int>();
            var acessos = new List<int>();

            foreach(AcessosHoraDto acessosHoraDto in listaAcessosHora)
            {
                horas.Add(acessosHoraDto.Hora);

                acessos.Add(acessosHoraDto.Quantidade);
            }

            ViewBag.Horas = horas;
            ViewBag.Acessos = acessos;

            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListaAcessosUsuario(int usuarioId)
        {
            var lista = HistoricoModel.RecurarListaAcessosUsuario(usuarioId);

            return Json(lista);
        }

   }
}
