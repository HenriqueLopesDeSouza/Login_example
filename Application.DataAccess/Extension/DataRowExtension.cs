using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Extension
{
    public static class DataRowExtension
    {
        public static bool ExisteCampo(this DataRow dataRow, string fieldName)
        {
            return dataRow.Table.Columns.Contains(fieldName);
        }

        public static string ValorString(this DataRow dataRow, string fieldName)
        {
            if (ExisteCampo(dataRow, fieldName))
                return Convert.ToString(dataRow[fieldName]);

            return null; // padrão
        }

        public static int ValorInt32(this DataRow dataRow, string fieldName)
        {
            return Convert.ToInt32(ValorInt64(dataRow, fieldName));
        }

        public static long ValorInt64(this DataRow dataRow, string fieldName)
        {
            if (ExisteCampo(dataRow, fieldName))
                if (!Convert.IsDBNull(dataRow[fieldName]))
                    return Convert.ToInt64(dataRow[fieldName]);
            return 0; // padrão
        }

        public static byte[] ValorByteArray(this DataRow dataRow, string nomeCampo)
        {
            if (ExisteCampo(dataRow, nomeCampo))
                if (!Convert.IsDBNull(dataRow[nomeCampo]))
                    return (byte[])dataRow[nomeCampo];
            return null; // padrão
        }

    }
}
