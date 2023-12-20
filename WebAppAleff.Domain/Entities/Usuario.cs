using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAleff.Domain.Entities
{
    public class Usuario
    {
        public Usuario()
        { }
        public Usuario(
            int usuarioId,
            string nome,
            string login = "",
            bool isAdmin = false,
            string senha = "")
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Login = login;
            IsAdmin = isAdmin;
            Senha = senha;
        }

        public int UsuarioId { get; private set; }
        public string Nome { get; private set; } = null;
        public string Login { get; private set; } = null;
        public string Senha { get; private set; } = null;
        public bool IsAdmin { get; private set; } = false;

    }
}
