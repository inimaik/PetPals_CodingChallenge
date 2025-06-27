using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class DbConnUtil
    {
        public static SqlConnection GetConnectionObject(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
