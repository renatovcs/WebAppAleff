using System;
using System.Configuration;

namespace WebAppAleff.Infra.Persistence
{
    public class SqlServerDBConnection
    {
        private static string connectionString { get; set; }

        private static string providerName { get; set; }


        static SqlServerDBConnection()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;

                providerName = ConfigurationManager.ConnectionStrings["SqlServer"].ProviderName;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao acessar o banco: " + ex);
            }
        }

        public static string ConnectionString
        {
            get { return connectionString; }
        }

        public static string ProviderName
        {
            get { return providerName; }
        }

    }
}
