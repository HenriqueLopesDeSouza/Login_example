using Microsoft.Data.SqlClient;
using System.Data;

namespace Application.DataAccess
{
    [Serializable]
    public sealed class Parameters
    {
        private List<SqlParameter> SqlParameters { get; set; }

        public Parameters()
        {
            this.SqlParameters = new List<SqlParameter>();
        }

        private void Add(string parameterName, object value)
        {
            this.SqlParameters.Add(new SqlParameter(parameterName, value));
        }

        public void AddOutPutParameter(string parameterName, SqlDbType sqlDbType, int tamanho)
        {
            if (!string.IsNullOrWhiteSpace(parameterName))
                this.SqlParameters.Add(new SqlParameter(parameterName, sqlDbType, tamanho, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Default, DBNull.Value));
        }

		public object RecoverParameterValue(string parameterName)
		{
			if (this.SqlParameters != null)
			{
				if (this.SqlParameters.Count > 0)
				{
					SqlParameter parametro = this.SqlParameters.Find(s => s.ParameterName == parameterName);
					if (parametro != null)
						return parametro.Value;
				}
			}
			return null;
		}

		public void AddString(string parameterName, string value)
		{
			this.Add(parameterName, value);
		}

		public void AddStringOptional(string parameterName, string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				this.Add(parameterName, DBNull.Value);
			else
				this.Add(parameterName, value);
		}

		public void AddInt32(string parameterName, int value)
		{
			this.Add(parameterName, value);
		}

		public void AddInt32Optional(string parameterName, int value)
		{
			if (value > 0)
				this.Add(parameterName, value);
			else
				this.Add(parameterName, DBNull.Value);
		}

		public void Clear()
		{
			this.SqlParameters.Clear();
		}

		public SqlParameter[] ToArray()
		{
			if (this.SqlParameters != null)
				return this.SqlParameters.ToArray();
			return null;
		}

	}
}
