using System;
using System.Data.SqlClient;

namespace QuanLySanPham.util
{
    public class ConnectDB
    {
        private static string conStr = "Server=localhost\\MSSQLSERVER13;Database=QLSP;User Id=sa;Password=admin@123;";
        private SqlConnection con = new SqlConnection(conStr);
        
        public SqlConnection GetConnection()
        {
            return new SqlConnection(conStr);
        }
    }
}
