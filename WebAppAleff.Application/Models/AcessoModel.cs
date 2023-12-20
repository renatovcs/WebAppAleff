using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAleff.Application.Services;
using WebAppAleff.Domain.Entities;

namespace WebAppAleff.Application.Models
{
    public class AcessoModel
    {
        public static Usuario Validar(string login, string senha)
        {
            return UsuarioService.Validar(login, senha);
        }

        public static void VerificaAdmin()
        {
            if (UsuarioService.TotalUsuarios() == 0)
            {
                UsuarioService.IncluirAdmin();
            }
        }

    }
}