using System;
using System.Data;
using System.Data.SqlClient;

namespace DemoHangfire.Helpers
{
    public static class SqlServerHelper
    {
        public static void WaitForSqlServer(string cs)
        {
            Console.WriteLine("Waiting for SQL Server...");
            var wait = true;

            var conn = new SqlConnection(cs);
            while (wait)
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        wait = false;
                    }
                }
                catch { }
            }
            conn.Dispose();

            Console.WriteLine("SQL Server is up.");
        }
    }
}
