using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace exam_pk111
{
    internal static class DBC
    {
        static String conStr = "Server=localhost;User ID=root;Password=root;Database=exam_pk111";
        static MySqlConnection conn = null;

        static public MySqlConnection GetConn()
        {
            conn = new MySqlConnection(conStr);
            try
            {
                conn.Open();
            } catch {
            
            }
            if(conn.State == System.Data.ConnectionState.Open) 
            {
                return conn;
            } else { 
                return null; 
            }
        }
        static public void CloseConn()
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
