using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppAleff.Domain.Entities;

namespace WebAppAleff.Application.Services
{
    public interface IUsuarioService<T>
    {
        void Incluir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);
        DataTable Consultar(string Nome);
        Usuario GetUsuarioId(int id);

    }
}
