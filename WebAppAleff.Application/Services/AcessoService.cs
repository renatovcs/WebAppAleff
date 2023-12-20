using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppAleff.Application.Dto;
using WebAppAleff.Domain.Entities;
using WebAppAleff.Infra.Persistence;

namespace WebAppAleff.Application.Services
{
    public class AcessoService
    {
        public static List<Acesso> Exibir(int usuarioId = 0)
        {
            return GetListaAcesso(ExibirTodos(usuarioId));
        }

        public static List<AcessosHoraDto> GetListAcessosHora(int usuarioId = 0)
        {
            var dt = new DataTable();

            var listaParametros = new List<DbParameter>();

            var sql = "SELECT DATEPART(HOUR, DataHoraAcesso) AS Hora, COUNT(*) AS TotalRegistros " +
                        "FROM LogAcesso WHERE DataHoraAcesso >= DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0) AND DataHoraAcesso<DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()) +1, 0) " +
                        ((usuarioId > 0) ? " AND UsuarioId = @usuarioId " : "") +
                        "GROUP BY DATEPART(HOUR, DataHoraAcesso) ORDER BY Hora; ";

            if (usuarioId > 0)
                listaParametros.Add(new SqlParameter("@usuarioId", usuarioId));

            dt = (DataTable)DataBase.ExecuteCommand(sql, CommandType.Text, listaParametros, TypeCommand.ExecuteDataTable);

            List<AcessosHoraDto> listaAcessosHora = new List<AcessosHoraDto>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drRow in dt.Rows)
                {
                    AcessosHoraDto _acessosHoraDto = new AcessosHoraDto(
                        Convert.ToInt32(drRow[0]),
                        Convert.ToInt32(drRow[1])
                        );

                    listaAcessosHora.Add(_acessosHoraDto);
                }

            }
            else
            {
                listaAcessosHora = null;
            }

            return listaAcessosHora;


        }


        public static List<Usuario> ListaUsuairosAcesso()
        {
            return GetListaUsuariosAcesso(RecurarUsuairosAcesso());
        }

        private static List<Usuario> GetListaUsuariosAcesso(DataTable tabela)
        {
            try
            {
                List<Usuario> listaUsuarios = new List<Usuario>();

                if (tabela.Rows.Count > 0)
                {
                    foreach (DataRow drRow in tabela.Rows)
                    {
                        Usuario _Usuario = new Usuario(
                            Convert.ToInt32(drRow[0]),
                            Convert.ToString(drRow[1])
                            );

                        listaUsuarios.Add(_Usuario);
                    }

                }
                else
                {
                    listaUsuarios = null;
                }

                return listaUsuarios;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static DataTable RecurarUsuairosAcesso()
        {
            var dt = new DataTable();

            var sql = "SELECT distinct la.UsuarioId, u.Nome FROM LogAcesso la JOIN Usuario u ON la.UsuarioId = u.UsuarioId ORDER BY u.Nome desc";

            dt = (DataTable)DataBase.ExecuteCommand(sql, CommandType.Text, null, TypeCommand.ExecuteDataTable);

            return dt;
        }

        private static List<Acesso> GetListaAcesso(DataTable tabela)
        {
            try
            {
                List<Acesso> listaAcessos = new List<Acesso>();

                if (tabela.Rows.Count > 0)
                {
                    foreach (DataRow drRow in tabela.Rows)
                    {
                        Acesso _Acesso = new Acesso(
                            Convert.ToInt32(drRow[0]),
                            Convert.ToInt32(drRow[1]),
                            drRow[2].ToString(),
                            drRow[3].ToString(),
                            (DateTime)drRow[4]
                            );

                        listaAcessos.Add(_Acesso);
                    }

                }
                else
                {
                    listaAcessos = null;
                }

                return listaAcessos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ExibirTodos(int usuarioId = 0)
        {
            var dt = new DataTable();

            var listaParametros = new List<DbParameter>();

            var sql = "SELECT la.LogAcessoId, la.UsuarioId, u.Nome, la.EnderecoIp, la.DataHoraAcesso " + 
                       "FROM LogAcesso la JOIN Usuario u ON la.UsuarioId = u.UsuarioId " +
                       ((usuarioId > 0) ? "WHERE u.UsuarioId = @usuarioId " : "") +
                       "ORDER BY la.DataHoraAcesso desc";

            if (usuarioId > 0)
                listaParametros.Add(new SqlParameter("@usuarioId", usuarioId));

            dt = (DataTable)DataBase.ExecuteCommand(sql, CommandType.Text, listaParametros, TypeCommand.ExecuteDataTable);

            return dt;
        }
    }
}
