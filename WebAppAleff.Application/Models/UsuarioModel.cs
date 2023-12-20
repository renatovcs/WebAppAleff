using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebAppAleff.Application.Services;
using WebAppAleff.Domain.Entities;

namespace WebAppAleff.Application.Models
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Informe o nome.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Informe um login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe uma senha.")]
        [RegularExpression(@"^(?!.*(.).*\1)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()-+]).{10,}$",
        ErrorMessage = "A senha deve ter pelo menos 10 caracteres, incluindo pelo menos 1 dígito numérico, 1 letra minúscula, 1 letra maiúscula, não possuir caracteres repetidos nem espaços em branco e ao menos 1 caractere especial como !@#$%^&*()-+.")]
        public string Senha { get; set; }

        public bool IsAdmin { get; set; }

        public static int Alterar(UsuarioModel usuarioModel)
        {

            Usuario usuario = new Usuario(
                usuarioModel.UsuarioId, 
                usuarioModel.Nome, 
                usuarioModel.Login, 
                usuarioModel.IsAdmin, 
                usuarioModel.Senha);

            return UsuarioService.Alterar(usuario);

        }

        public static int Incluir(UsuarioModel usuarioModel)
        {

            Usuario usuario = new Usuario(
                usuarioModel.UsuarioId,
                usuarioModel.Nome,
                usuarioModel.Login,
                usuarioModel.IsAdmin,
                usuarioModel.Senha);

            return UsuarioService.Incluir(usuario);

        }

        public static List<UsuarioModel> RecuperarLista()
        {

            List<UsuarioModel> listaUsuarioModel = new List<UsuarioModel>();

            var listaUsuario = UsuarioService.Exibir();

            foreach(Usuario usu in listaUsuario)
            {
                UsuarioModel _UsuarioModel = new UsuarioModel();
                _UsuarioModel.UsuarioId = usu.UsuarioId;
                _UsuarioModel.Nome = usu.Nome;
                _UsuarioModel.Login = usu.Login;
                _UsuarioModel.IsAdmin = usu.IsAdmin;

                listaUsuarioModel.Add(_UsuarioModel);
            }

            return listaUsuarioModel;
        }

        public static UsuarioModel RecuperarUsuario(int usuarioId)
        {
            Usuario usuario = UsuarioService.GetUsuarioId(usuarioId);

            UsuarioModel usuarioModel = new UsuarioModel
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Login = usuario.Login,
                IsAdmin = usuario.IsAdmin
            };

            return usuarioModel;
        }

        public static bool Excluir(int usuarioId)
        {

            return UsuarioService.Excluir(usuarioId);

        }


    }
}
