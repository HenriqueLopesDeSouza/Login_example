using Application.Business.Interfaces;
using Application.Business.Models;
using Application.Business.Models.Enumerators;
using Application.DataAccess;
using Application.DataAccess.Extension;
using Application.DataAccess.Interfaces;
using System.Data;

namespace Application.Data.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private const string USP_DELETE_TOKEN = "USP_DELETE_TOKEN";
        private const string USP_SELECT_TOKEN = "USP_SELECT_TOKEN";
        private const string USP_INSERT_TOKEN = "USP_INSERT_TOKEN";

		private readonly IDataAccessFactory _dataAccessFactory;

        public TokenRepository(IDataAccessFactory dataAccessFactory)
        {
            _dataAccessFactory = dataAccessFactory;
        }

        public Tokens? GetToken(string? username = null, string? token = null)
		{
			Parameters parametros = new();
			parametros.AddStringOptional("@TOKEN", token);
			parametros.AddStringOptional("@USERNAME", username);

			var dataTable = _dataAccessFactory.GetDataAccessConnection().ExecuteDataTable
				(USP_SELECT_TOKEN, CommandType.StoredProcedure, parametros);

			List<Tokens> list = new();

			if (dataTable.Rows != null && dataTable.Rows.Count > 0)
				foreach (DataRow row in dataTable.Rows)
				{
					list.Add(new Tokens
					{
						Token = row.ValorString("TOKEN"),
					});
				}

			if (list.Count > 0)
				return list[0];
			else
				return null;
		}


		public Result InsertToken(string username, string token, out string message)
		{
			try
			{
				Parameters parameters = new();
				parameters.AddString("@TOKEN", token);
				parameters.AddString("@USER_NAME", username);
			
				parameters.AddOutPutParameter("@RESULT", SqlDbType.Int, 1);
				parameters.AddOutPutParameter("@MESSAGE", SqlDbType.VarChar, 2000);

				var dataTable = _dataAccessFactory.
								GetDataAccessConnection().
								ExecuteScalar(USP_INSERT_TOKEN, CommandType.StoredProcedure, parameters.ToArray());

				message = parameters.RecoverParameterValue("@MESSAGE").ToString();
				int result = Convert.ToInt32(parameters.RecoverParameterValue("@RESULT"));

				return (Result)result;

			}
			catch (Exception ex)
			{
				message = ex.Message;
				int result = -1;
				return (Result)result;

			}
		}

		public Result DeleteToken(out string message, string? username = null, string? refreshToken = null)
		{
			try
			{
				Parameters parameters = new();
				parameters.AddStringOptional("@TOKEN", refreshToken);
				parameters.AddStringOptional("@USERNAME", username);

				parameters.AddOutPutParameter("@RESULT", SqlDbType.Int, 1);
				parameters.AddOutPutParameter("@MESSAGE", SqlDbType.VarChar, 2000);

				var dataTable = _dataAccessFactory.
								GetDataAccessConnection().
								ExecuteScalar(USP_DELETE_TOKEN, CommandType.StoredProcedure, parameters.ToArray());

				message = parameters.RecoverParameterValue("@MESSAGE").ToString();
				int result = Convert.ToInt32(parameters.RecoverParameterValue("@RESULT"));

				return (Result)result;

			}
			catch (Exception ex)
			{
				message = ex.Message;
				int result = -1;
				return (Result)result;

			}
		}
	}
}
