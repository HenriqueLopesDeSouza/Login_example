using Application.Business.Models;
using Application.DataAccess.Interfaces;

namespace Application.DataAccess
{
    public class DataAccessFactory : IDataAccessFactory
    {
       
        public DataAccess GetDataAccessConnection()
        {
            var conectionString = Settings.MainConnection;
            return new DataAccessSql(conectionString);
        }
    }
}
