using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HAMS.DataUtil
{
    class DBUtil
    {
        private static MySqlConnection conn;
        private static void init()
        {
            try
            {//进行数据库的连接操作
                string constr = "server=182.92.220.26;Uid=HAMS;password=HAMS;Database=HAMS;Charset=utf8;Allow User Variables = True";
                conn = new MySqlConnection(constr);
                //打开数据库连接
                conn.Open();
                //判断数据库连接是否成功
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("数据库连接成功");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //静态方法来获得数据库的连接
        public static MySqlConnection getConnection()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                return conn;
            }
            init();
            return conn;

        }
    }
}
