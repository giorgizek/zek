/*using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace Zek.Data.Sql
{
    public class SqlHelper : DbHelper
    {
        #region private utility methods & constructors

        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new Sql()"
        private SqlHelper() { }

        /// <summary>
        /// This method is used to attach array of SqlParameters to a SqlCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (commandParameters != null)
            {
                foreach (var p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters
        /// </summary>
        /// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the SqlParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                var dbDataParameter = parameterValues[i] as IDbDataParameter;
                if (dbDataParameter != null)
                {
                    var paramInstance = dbDataParameter;
                    commandParameters[i].Value = paramInstance.Value ?? DBNull.Value;
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The SqlCommand to be prepared</param>
        /// <param name="connection">A valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">A valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, int? commandTimeout, IEnumerable<SqlParameter> commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            if (commandTimeout != null && commandTimeout.Value > 0)
                command.CommandTimeout = commandTimeout.Value;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        #endregion private utility methods & constructors

        #region ExecuteNonQuery

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored prcedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(SqlConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataTable(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataTable(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(SqlTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataTable(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataTable(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion

      

        #region ExecuteReader

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = SqlParameterCache.GetSpParameterSet(connectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = SqlParameterCache.GetSpParameterSet(connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }



        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = SqlParameterCache.GetSpParameterSet(transaction.Connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            // Create & open a SqlConnection, and dispose of it after we are done
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteScalar(connection, commandType, commandText, commandTimeout, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();

            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            var retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            var retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = SqlParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteScalar


        #region ExecuteList
        public static List<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            return DataReaderMapToList<T>(ExecuteReader(connectionString, commandType, commandText, commandTimeout, commandParameters));
        }
        public static List<T> ExecuteList<T>(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToList<T>(ExecuteReader(connectionString, spName, commandTimeout, parameterValues));
        }
        public static List<T> ExecuteList<T>(SqlConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            return DataReaderMapToList<T>(ExecuteReader(connection, commandType, commandText, commandTimeout, commandParameters));
        }
        public static List<T> ExecuteList<T>(SqlConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToList<T>(ExecuteReader(connection, spName, commandTimeout, parameterValues));
        }
        public static List<T> ExecuteList<T>(SqlTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            return DataReaderMapToList<T>(ExecuteReader(transaction, commandType, commandText, commandTimeout, commandParameters));
        }
        public static List<T> ExecuteList<T>(SqlTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToList<T>(ExecuteReader(transaction, spName, commandTimeout, parameterValues));
        }
        #endregion


        #region ExecuteDictionary
        public static Dictionary<TKey, TValue> ExecuteDictionary<TKey, TValue>(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            return DataReaderMapToDictionary<TKey, TValue>(ExecuteReader(connectionString, commandType, commandText, commandTimeout, commandParameters));
        }
        public static Dictionary<TKey, TValue> ExecuteDictionary<TKey, TValue>(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToDictionary<TKey, TValue>(ExecuteReader(connectionString, spName, commandTimeout, parameterValues));
        }
        public static Dictionary<TKey, TValue> ExecuteDictionary<TKey, TValue>(SqlConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            return DataReaderMapToDictionary<TKey, TValue>(ExecuteReader(connection, commandType, commandText, commandTimeout, commandParameters));
        }
        public static Dictionary<TKey, TValue> ExecuteDictionary<TKey, TValue>(SqlConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToDictionary<TKey, TValue>(ExecuteReader(connection, spName, commandTimeout, parameterValues));
        }
        public static Dictionary<TKey, TValue> ExecuteDictionary<TKey, TValue>(SqlTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params SqlParameter[] commandParameters)
        {
            return DataReaderMapToDictionary<TKey, TValue>(ExecuteReader(transaction, commandType, commandText, commandTimeout, commandParameters));
        }
        public static Dictionary<TKey, TValue> ExecuteDictionary<TKey, TValue>(SqlTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToDictionary<TKey, TValue>(ExecuteReader(transaction, spName, commandTimeout, parameterValues));
        }
        #endregion





        /// <summary>
        /// ქმნის ახალ ტრანსაქციას.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static SqlTransaction BeginTransaction(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn.BeginTransaction();
        }
        /// <summary>
        /// აქომიტებს და ხურავს კავშირს ბაზასთან.
        /// </summary>
        /// <param name="tran"></param>
        public static void Commit(SqlTransaction tran)
        {
            tran.Commit();
            CloseConnection(tran);
        }
        /// <summary>
        /// ხურავს კავშირს ბაზასთან.
        /// </summary>
        /// <param name="tran"></param>
        public static void CloseConnection(SqlTransaction tran)
        {
            tran.Connection?.Close();
        }

        /// <summary>
        /// ხურავს კავშირს ბაზასთან.
        /// </summary>
        /// <param name="cmd"></param>
        public static void CloseConnection(SqlCommand cmd)
        {
            cmd.Connection?.Close();
        }

        
        #region Filter
        /// <summary>
        /// ქმნის დაცულ პარამეტრს (მაგ. თუ თარიღი ძალიან დაბალი ან მაქსიმალურია...).
        /// </summary>
        /// <param name="dbParameter">Sql პარამეტრი.</param>
        public static void DoSafeParameter(ref SqlParameter dbParameter)
        {
            if (dbParameter.Direction == ParameterDirection.Input || dbParameter.Direction == ParameterDirection.InputOutput)
            {
                if (dbParameter.Value == null)
                {
                    dbParameter.Value = DBNull.Value;
                }
                else if (dbParameter.SqlDbType == SqlDbType.DateTime)
                {
                    if ((DateTime)dbParameter.Value < SqlDateTime.MinValue.Value)
                    {
                        dbParameter.Value = SqlDateTime.MinValue.Value;
                    }
                    else if ((DateTime)dbParameter.Value > SqlDateTime.MaxValue.Value)
                    {
                        dbParameter.Value = SqlDateTime.MaxValue.Value;
                    }
                }
            }
        }
        public static string SafeSqlLiteral(string strValue)
        {
            strValue = strValue.Replace("'", "''"); // Most important one! This line alone can prevent most injection attacks
            strValue = strValue.Replace("--", "").Replace("[", "[[]").Replace("%", "[%]").Replace(" OR ", "").Replace(" or ", "");
            strValue = strValue.Replace(" AND ", "").Replace(" and ", "").Replace("\\\r\n", "").Replace("\\\r\n\r\n", "");

            var myArray = new[] { "xp_ ", "update ", "insert ", "select ", "drop ", "alter ", "create ", "rename ", "delete ", "replace " };
            for (var i = 0; i < myArray.Length; i++)
            {
                var strWord = myArray[i];
                var rx = new Regex(strWord, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var matches = rx.Matches(strValue);
                var i2 = 0;
                foreach (Match match in matches)
                {
                    var groups = match.Groups;
                    var intLenghtLeft = groups[0].Index + myArray[i].Length + i2;
                    strValue = strValue.Substring(0, intLenghtLeft - 1) + "&nbsp;" + strValue.Substring(strValue.Length - (strValue.Length - intLenghtLeft), strValue.Length - intLenghtLeft);
                    i2 += 5;
                }
            }
            return strValue;
        }
        #endregion
    }
}*/
