using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAleff.Domain.Entities
{
    public sealed class Acesso
    {
        public Acesso() 
        { }

        public Acesso(
            int logAcessoId,
            int usuarioId,
            string nomeUsuario,
            string enderecoIp,
            DateTime dataHoraAcesso) 
        {
            LogAcessoId = logAcessoId;
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            EnderecoIp = enderecoIp;
            DataHoraAcesso = dataHoraAcesso;
        }

        public int LogAcessoId { get; private set; }
        public int UsuarioId { get; private set; }
        public string NomeUsuario { get; private set; }
        public string EnderecoIp { get; private set; }
        public DateTime DataHoraAcesso { get; private set; }

    }
}