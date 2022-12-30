using System.Data;
using Microsoft.Data.SqlClient;


namespace Application.DataAccess
{
    public  class DataAccessSql : DataAccess
	{
		internal DataAccessSql(string stringConexao)
		{
			this.StringConnection = stringConexao;
		}


        internal override IDbConnection GetConnection()
        {
			return new SqlConnection();
		}

		internal override IDbCommand GetCommand()
        {
			return new SqlCommand();
		}

		internal override IDataParameter GetParameterReturn()
        {
			SqlParameter p = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
			p.Direction = ParameterDirection.ReturnValue;

			return p;
		}

        internal override IDbDataAdapter GetDataAdapter()
        {
			return new SqlDataAdapter();
		}
	}
}
