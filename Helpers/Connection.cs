using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Connection
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection ConnectionFactory()
        {
            return new SqlConnection($@"{ConnectionString}");
        }
    }
}
