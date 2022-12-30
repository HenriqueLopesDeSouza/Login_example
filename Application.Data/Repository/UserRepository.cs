using Application.Business.Interfaces;
using Application.Business.Models;
using Application.Business.Models.Enumerators;
using Application.DataAccess;
using Application.DataAccess.Extension;
using Application.DataAccess.Interfaces;
using System.Data;


namespace Application.Data.Repository
{
	public class UserRepository : IUserRepository
	{
		private const string USP_INSERT_USER = "USP_INSERT_USER";
		private const string USP_SELECT_USERS = "USP_SELECT_USERS";
		private const string USP_RETURN_PASSWORD_AND_SALT = "USP_RETURN_PASSWORD_AND_SALT";

		private readonly IDataAccessFactory _dataAccessFactory;

		public UserRepository(IDataAccessFactory dataAccessFactory)
		{
			_dataAccessFactory = dataAccessFactory;
		}

		public User? Login(string userName)
		{
			Parameters parametros = new();
			parametros.AddStringOptional("@USER_NAME", userName);

			var dataTable = _dataAccessFactory.GetDataAccessConnection().ExecuteDataTable
				(USP_RETURN_PASSWORD_AND_SALT, CommandType.StoredProcedure, parametros);

			List<User> user = new();

			if (dataTable.Rows != null && dataTable.Rows.Count > 0)
				foreach (DataRow row in dataTable.Rows)
				{
					user.Add(new User
					{
						UserName = row.ValorString("USER_NAME"),
						Password = row.ValorString("PASSWORD"),
						Email = row.ValorString("EMAIL"),
						Name = row.ValorString("NAME"),
						Role = row.ValorString("ROLE"),
						Salt = row.ValorString("SALT"),
					});
				}

			if (user.Count > 0)
				return user[0];
			else 
				return null;
		}

		public List<User> Get()
		{
			Parameters parametros = new();
			parametros.AddStringOptional("@NAME",null);
			parametros.AddStringOptional("@EMAIL", null);
			parametros.AddStringOptional("@USER_NAME", null);

			var dataTable = _dataAccessFactory.GetDataAccessConnection().ExecuteDataTable
				(USP_SELECT_USERS, CommandType.StoredProcedure, parametros);

			List<User> listaFuncao = new();

            if (dataTable.Rows != null && dataTable.Rows.Count > 0)
                foreach (DataRow row in dataTable.Rows)
                {
                    listaFuncao.Add(new User
                    {
						Name = row.ValorString("NAME"),
						UserName = row.ValorString("USER_NAME"),
						Password = row.ValorString("PASSWORD"),
						Email = row.ValorString("EMAIL"),
						Role = row.ValorString("ROLE"),
					});
                }

            return listaFuncao;
		}


		public Result InsertUser(User user,out string? message)
		{
			try 
			{
				Parameters parameters = new();
				parameters.AddString("@NAME", user.Name);
				parameters.AddString("@EMAIL", user.Email);
				parameters.AddString("@USER_NAME", user.UserName);
				parameters.AddString("@PASSWORD", user.Password);
				parameters.AddString("@ROLE", user.Role);
				parameters.AddString("@SALT", user.Salt);
				parameters.AddOutPutParameter("@RESULT", SqlDbType.Int, 1);
				parameters.AddOutPutParameter("@MESSAGE", SqlDbType.VarChar, 2000);

				var dataTable = _dataAccessFactory.
								GetDataAccessConnection().
								ExecuteScalar(USP_INSERT_USER, CommandType.StoredProcedure, parameters.ToArray());

				message = parameters.RecoverParameterValue("@MESSAGE").ToString();
				int result = Convert.ToInt32(parameters.RecoverParameterValue("@RESULT"));

				return (Result)result;

			} catch (Exception ex) 
			{
				message =  ex.Message;
				int result = -1;
				return (Result)result;

			}
		}		



	}
}
