using System.Data;
using Microsoft.Data.SqlClient;


namespace Application.DataAccess
{
    public abstract class DataAccess
    {
        private string stringConnection;
        private IDbConnection connection;
        private IDbCommand command;
        private IDbTransaction transaction;
        private IDbDataAdapter dataAdapter;
        private int commandTimeout;

        internal string StringConnection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(stringConnection))
                    throw new ArgumentException("Invalid connection string.");

                return stringConnection;
            }
            set { stringConnection = value; }
        }

        internal abstract IDbConnection GetConnection();
        internal abstract IDbCommand GetCommand();
        internal abstract IDbDataAdapter GetDataAdapter();
        internal abstract IDataParameter GetParameterReturn();

        public void SetTimeOut(int timeOut)
        {
            commandTimeout = timeOut;
        }

        public void StartTransaction()
        {
            try
            {
                connection = GetConnection();
                connection.ConnectionString = this.StringConnection;
                connection.Open();
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void EndTransaction()
        {
            if (transaction == null)
                return;

            try
            {
                transaction.Commit();
            }

            catch
            {
                RollbackTransaction();
                throw;
            }

            finally
            {
                connection.Close();
                transaction = null;
            }

        }

        public void RollbackTransaction()
        {
            if (transaction == null)
                return;

            try
            {
                transaction.Rollback();
            }

            catch
            {
                throw;
            }

            finally
            {
                connection.Close();
                transaction = null;
            }
        }

        public object ExecuteScalar(string commandText, CommandType commandType, Parameters parametros)
        {
            return this.ExecuteScalar(commandText, commandType, this.GetParametros(parametros));
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, Parameters parametros)
        {
            return this.ExecuteDataTable(commandText, commandType, this.GetParametros(parametros));
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, IDataParameter[] cmdParametros)
        {
            return this.ExecuteDataTable(commandText, commandType, cmdParametros, 0);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, IDataParameter[] cmdParametros, int timeoutComando)
        {
            try
            {
                PrepareDataAdapter(commandType, commandText, cmdParametros, timeoutComando);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (transaction == null)
                    if (connection != null)
                        connection.Close();
                    else
                        RollbackTransaction();

                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (dataAdapter != null)
                {
                    dataAdapter.SelectCommand.Connection.Close();
                    dataAdapter.SelectCommand.Connection.Dispose();
                }
            }
        }

        public object ExecuteScalar(string commandText, CommandType commandType, IDataParameter[] cmdParametros)
        {
            try
            {
                PrepareCommand(commandType, commandText, cmdParametros);

                object objeto = command.ExecuteScalar();

                if (objeto != DBNull.Value)
                    return objeto;
                else
                    return null;
            }
            catch
            {
                if (transaction != null)
                    RollbackTransaction();

                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    command.Dispose();
                }
            }
        }

        private void PrepareDataAdapter(CommandType commandType, string commandText, IDataParameter[] cmdParametros, int timeoutComando)
        {
            dataAdapter = GetDataAdapter();
            dataAdapter.SelectCommand = GetCommand();
            dataAdapter.SelectCommand.Connection = GetConnection();
            dataAdapter.SelectCommand.Connection.ConnectionString = StringConnection;
            dataAdapter.SelectCommand.CommandText = commandText;
            dataAdapter.SelectCommand.CommandType = commandType;
            if (timeoutComando > 0)
                dataAdapter.SelectCommand.CommandTimeout = timeoutComando;
            if (cmdParametros != null)
            {
                foreach (IDataParameter parametros in cmdParametros)
                    dataAdapter.SelectCommand.Parameters.Add(parametros);
            }
        }

        private void PrepareCommand(CommandType commandType, string commandText, IDataParameter[] cmdParametros)
        {
            if (connection == null)
            {
                connection = GetConnection();
                connection.ConnectionString = this.StringConnection;
            }

            if (connection.State != ConnectionState.Open)
                connection.Open();

            if (command == null)
                command = GetCommand();

            if (this.commandTimeout > 0)
                command.CommandTimeout = this.commandTimeout;

            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
                command.Transaction = transaction;

            command.Parameters.Clear();

            if (cmdParametros != null)
            {
                command.Parameters.Add(this.GetParameterReturn());
                foreach (IDataParameter parametros in cmdParametros)
                    command.Parameters.Add(parametros);
            }
        }

        private SqlParameter[] GetParametros(Parameters parametros)
        {
            if (parametros == null)
                throw new ApplicationException("Data Access: The Parameters object is null.");
            return parametros.ToArray();
        }

    }
}
