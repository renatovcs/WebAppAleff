using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAppAleff.Application.Models;
using WebAppAleff.Domain.Entities;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using Serilog;
using Serilog.Core;

namespace WebAppAleff.API.Controllers
{
    public class UsuarioController : ApiController
    {

        public IHttpActionResult Get()
        {
            var lista = UsuarioModel.RecuperarLista();
            
            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("GET {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            return Ok(lista);
        }


        public IHttpActionResult Get(int id)
        {

            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("GET {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));


            var usuario = UsuarioModel.RecuperarUsuario(id);

            if (usuario != null) 
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }

        }

        public IHttpActionResult Post([FromBody] UsuarioModel value)
        {

            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("POST {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            try
            {
                var ret = UsuarioModel.Incluir(value);

                if (ret > 0) 
                {
                    return Ok(ret);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Put([FromBody] UsuarioModel value)
        {

            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("PUT {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));


            try
            {
                var ret = UsuarioModel.Alterar(value);

                if (ret > 0)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Delete(int id)
        {

            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("DELETE {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));


            try
            {
                if (UsuarioModel.Excluir(id))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/acesso/lista")]
        public IHttpActionResult ListaAcessosUsuairo(int id)
        {

            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("GET {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var lista = HistoricoModel.AcessosHora(id);

            if (lista != null)
            {
                return Ok(lista);
            }
            else
            {
                return NotFound();
            }

        }

    }
}
