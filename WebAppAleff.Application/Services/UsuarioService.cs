using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using WebAppAleff.Application.Helpers;
using WebAppAleff.Domain.Entities;
using WebAppAleff.Infra.Persistence;

namespace WebAppAleff.Application.Services
{
    public class UsuarioService 
    {
        public static int Alterar(Usuario usuario)
        {
            var query = "UPDATE Usuario SET Nome = @nome, Login = @login, IsAdmin = @isadmin " +
                (!string.IsNullOrEmpty(usuario.Senha) ? ", Senha = @senha " : "") +
                "WHERE UsuarioId = @usuarioid ";

            List<DbParameter> listaParametros = new List<DbParameter>
            {
                new SqlParameter("@nome", usuario.Nome),
                new SqlParameter("@login", usuario.Login),
                new SqlParameter("@isadmin", usuario.IsAdmin),
                new SqlParameter("@usuarioid", usuario.UsuarioId)
            };

            if (!string.IsNullOrEmpty(usuario.Senha))
                listaParametros.Add(new SqlParameter("@senha", Cripto.HashMD5(usuario.Senha)));
            
            try 
            {
                if ((int)DataBase.ExecuteCommand(query, CommandType.Text, listaParametros, TypeCommand.ExecuteNonQuery) > 0)
                    return usuario.UsuarioId;
                else
                    return 0;
            }
            catch
            {
                return 0;

            }

        }

        public static Usuario Validar(string login, string senha)
        {
            try
            {
                string sql = string.Format("SELECT UsuarioId, Nome, IsAdmin FROM Usuario WHERE login = @login and senha = @senha");

                List<DbParameter> listaParametros = new List<DbParameter>
                {
                    new SqlParameter("@Login", login),
                    new SqlParameter("@Senha", Cripto.HashMD5(senha)),
                };

                DataTable dt = new DataTable();

                dt = (DataTable)DataBase.ExecuteCommand(sql, CommandType.Text, listaParametros, TypeCommand.ExecuteDataTable);

                Usuario usu = null;

                foreach (DataRow item in dt.Rows)
                {
                    usu = new Usuario(int.Parse(item["UsuarioId"].ToString()), item["Nome"].ToString(), login, item["IsAdmin"].ToString() == "True");

                    LogAcesso(usu);

                }

                return usu;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void LogAcesso(Usuario usuario)
        {
            var query = "INSERT INTO LogAcesso(UsuarioId, DataHoraAcesso, EnderecoIp) VALUES (@usuarioid, @datahoraacesso, @enderecoip)";

            List<DbParameter> listaParametros = new List<DbParameter>
            {
                new SqlParameter("@usuarioid", usuario.UsuarioId),
                new SqlParameter("@datahoraacesso", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@enderecoip", HttpContext.Current.Request.UserHostAddress)

            };

            DataBase.ExecuteCommand(query, CommandType.Text, listaParametros, TypeCommand.ExecuteNonQuery);

        }

        public static bool Excluir(int usuarioId)
        {
            // o ideal seria uma exclusão lógica, necessariamente alterando a estrutura de dados exigida.
            
            var query = string.Format("DELETE FROM Usuario WHERE UsuarioId = @usuarioid");

            List<DbParameter> listaParametros = new List<DbParameter>
            {
                new SqlParameter("@usuarioid", usuarioId)
            };

            try
            {
                DataBase.ExecuteCommand(query, CommandType.Text, listaParametros, TypeCommand.ExecuteNonQuery);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public static List<Usuario> Exibir()
        {
            return GetListaUsuario(ExibirTodos());
        }

        private static List<Usuario> GetListaUsuario(DataTable tabela)
        {
            try
            {
                List<Usuario> listaUsuario = new List<Usuario>();

                dynamic registros = tabela.Rows.Count;

                if (registros > 0)
                {
                    foreach (DataRow drRow in tabela.Rows)
                    {
                        Usuario _Usuario = new Usuario(
                            Convert.ToInt32(drRow[0]),
                            Convert.ToString(drRow["Nome"]),
                            drRow[2].ToString(),
                            drRow[3].ToString() == "True"
                            );

                        listaUsuario.Add(_Usuario);
                    }

                    return listaUsuario;
                }
                else
                {
                    listaUsuario = null;
                    return listaUsuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int TotalUsuarios()
        {
            var dt = ContarTodos();

            return dt.Rows.Count;
        }

        public static DataTable ContarTodos()
        {
            var dt = new DataTable();

            var cmd = "sp_ListaUsuario";

            dt = (DataTable)DataBase.ExecuteCommand(cmd, CommandType.StoredProcedure, null, TypeCommand.ExecuteDataTable);

            return dt;
        }

        public static DataTable ExibirTodos()
        {
            var dt = new DataTable();

            var cmd = "sp_ListaUsuario";

            dt = (DataTable)DataBase.ExecuteCommand(cmd, CommandType.StoredProcedure, null, TypeCommand.ExecuteDataTable);

            return dt;
        }


        public static Usuario GetUsuarioId(int id)
        {
            try
            {
                string sql = "SELECT UsuarioId, Nome, Login, IsAdmin FROM Usuario WHERE UsuarioId = @usuarioid";

                List<DbParameter> listaParametros = new List<DbParameter>
                {
                    new SqlParameter("@usuarioid", id)
                };

                DataTable dt = new DataTable();

                dt = (DataTable)DataBase.ExecuteCommand(sql, CommandType.Text, listaParametros, TypeCommand.ExecuteDataTable);


                Usuario usu = null;

                //dt.Rows.Count

                foreach (DataRow item in dt.Rows)
                {
                    usu = new Usuario(int.Parse(item["UsuarioId"].ToString()), item["Nome"].ToString(), item["Login"].ToString(), item["IsAdmin"].ToString() == "True" );
                }

                return usu;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Incluir(Usuario usuario)
        {
            var query = "INSERT INTO Usuario (Nome, Login, Senha, IsAdmin) VALUES (@Nome, @Login, @Senha, @IsAdmin); select convert(int, scope_identity())";

            List<DbParameter> listaParametros = new List<DbParameter>
            {
                new SqlParameter("@Nome", usuario.Nome),
                new SqlParameter("@Login", usuario.Nome),
                new SqlParameter("@Senha", Cripto.HashMD5(usuario.Senha)),
                new SqlParameter("@IsAdmin", usuario.IsAdmin)
            };

            try
            {
                return (int)DataBase.ExecuteCommand(query, CommandType.Text, listaParametros, TypeCommand.ExecuteScalar);
            }
            catch
            {
                return 0;
            }
            

        }

        public static void IncluirAdmin()
        {
            // aqui não uso a lista de parametros visto não ter parâmetros informados pelo usuário

            var query = string.Format("INSERT INTO Usuario(Nome, Login, Senha, IsAdmin) VALUES ('{0}','{1}','{2}','{3}')", "Administrador", "admin", Cripto.HashMD5("admin"), "1");

            DataBase.ExecuteCommand(query, CommandType.Text, null, TypeCommand.ExecuteNonQuery);
        }

        public static bool ValidarSenha(string senha)
        {
            string padrao = @"^(?!.*(.).*\1)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()-+]).{10,}$";

            return Regex.IsMatch(senha, padrao);
        }

    }
}
