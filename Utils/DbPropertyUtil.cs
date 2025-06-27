using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class DbPropertyUtil
    {
        public static string GetConnectionString()
        {
            return @"server=INIMAI\SQLEXPRESS;database=PetPalsDb;Integrated Security=true;TrustServerCertificate=true";
        }
    }
}
