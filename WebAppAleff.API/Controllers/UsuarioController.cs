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
using System.Threading.Tasks;
using System.Net.Http;

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

        [HttpGet]
        [Route("api/empregados30mais")]
        public async Task<IHttpActionResult> GetEmpregados30Mais()
        {

            var url = Request.RequestUri.AbsoluteUri;

            Log.Information("GET {url} {datahora}", url, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));


            string apiUrl = "https://dummy.restapiexample.com/api/v1/employees";
            
            List<Empregado> empregados30mais = new List<Empregado>();

            using (HttpClient client = new HttpClient())
            {
                // chamada da api externa
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ApiResult>();

                    // empregados com mais de 30 anos
                    empregados30mais = result?.data?.FindAll(emp => emp.employee_age > 30);
                }
            }

            return Ok(empregados30mais.Select(emp => new
            {
                id = emp.id,
                employee_name = emp.employee_name,
                employee_age = emp.employee_age
            }));
        }
    }

    public class ApiResult
    {
        public string status { get; set; }
        public List<Empregado> data { get; set; }
    }

    public class Empregado
    {
        public string id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }
    }

}