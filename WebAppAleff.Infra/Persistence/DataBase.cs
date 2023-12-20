using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;

namespace WebAppAleff.Infra.Persistence
{
    public class DataBase
    {
        public static DbCommand CreateCommand(string cmdText, CommandType commType, List<DbParameter> listParameter)
        {
            var factory = DbProviderFactories.GetFactory(SqlServerDBConnection.ProviderName);

            var conn = factory.CreateConnection();

            conn.ConnectionString = SqlServerDBConnection.ConnectionString;

            var comm = conn.CreateCommand();

            comm.CommandText = cmdText;

            comm.CommandType = commType;

            if(listParameter != null)
            {
                foreach (var param in listParameter)
                {
                    comm.Parameters.Add(param);
                }
            }

            return comm;
        }

        public static DbParameter CreateParameter (string nameParameter, DbType typeParameter, object valueParameter)
        {
            var factory = DbProviderFactories.GetFactory(SqlServerDBConnection.ProviderName);

            var param = factory.CreateParameter();

            if (param != null)
            {
                param.ParameterName = nameParameter;
                param.DbType = typeParameter;
                param.Value = valueParameter;
            }

            return param;
        }

        public static Object ExecuteCommand (string cmdText, CommandType cmdType, List<DbParameter> listParameter, TypeCommand typeCmd)
        {
            var command = CreateCommand(cmdText, cmdType, listParameter);
            
            Object objretorno = null;

            try
            {
                command.Connection.Open();

                switch (typeCmd)
                {
                    case TypeCommand.ExecuteNonQuery:
                        objretorno = command.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        objretorno = command.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        objretorno = command.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        var table = new DataTable();
                        var reader = command.ExecuteReader();

                        table.Load(reader);
                        reader.Close();

                        objretorno = table;
                        break;
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro ao executar o comando");
            }
            finally
            {
                if(typeCmd != TypeCommand.ExecuteReader)
                {
                    if (command.Connection.State == ConnectionState.Open)
                    {
                        command.Connection.Close();
                    }

                    command.Connection.Dispose();
                }
            }

            return objretorno;

        }

    }

    public enum TypeCommand
    {
        ExecuteNonQuery
        ,ExecuteReader
        ,ExecuteScalar
        ,ExecuteDataTable
    }
}
