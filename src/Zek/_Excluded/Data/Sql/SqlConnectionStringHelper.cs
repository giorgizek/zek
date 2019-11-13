//using System.Data.SqlClient;

namespace Zek.Data.Sql
{
    /*
    /// <summary>
    /// SQL-ის კონექშენის დამხმარე კლასი.
    /// </summary>
    public class SqlConnectionStringHelper
    {
        #region ConnectionString
        public static string GetConnectionString(string serverName, string databaseName, bool persistSecurityInfo = true, int? connectTimeout = null)
        {
            return GetConnectionString(serverName, databaseName, null, null, null, persistSecurityInfo, connectTimeout);
        }
        public static string GetConnectionString(string serverName, string databaseName, string username, string password, string applicationName, int? connectTimeout = null)
        {
            return GetConnectionString(serverName, databaseName, username, password, applicationName, true, connectTimeout);
        }
        public static string GetConnectionString(string serverName, string databaseName, string username, string password = null, string applicationName = null, bool persistSecurityInfo = true, int? connectTimeout = null)
        {
            if (string.IsNullOrWhiteSpace(serverName) || string.IsNullOrWhiteSpace(databaseName))
                return null;

            var connectionStringBuilder = new SqlConnectionStringBuilder { DataSource = serverName, InitialCatalog = databaseName };

            if (string.IsNullOrWhiteSpace(username))
                connectionStringBuilder.IntegratedSecurity = true;
            else
            {
                connectionStringBuilder.UserID = username;
                if (!string.IsNullOrWhiteSpace(password))
                    connectionStringBuilder.Password = password;
            }

            if (!string.IsNullOrWhiteSpace(applicationName))
                connectionStringBuilder.ApplicationName = applicationName;

            connectionStringBuilder.PersistSecurityInfo = persistSecurityInfo;

            if (connectTimeout != null)
                connectionStringBuilder.ConnectTimeout = connectTimeout.Value;

            return connectionStringBuilder.ConnectionString;
        }
        #endregion
    }*/
}
