using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace HAMS.DataUtil
{
    class DataOperation
    {
      private  static MySqlConnection conn = DataUtil.DBUtil.getConnection();
        //定义静态数据查询函数,此处可能不止有一个参数,params声明的是未知长度的数组
        public static DataTable DataQuery(string sql, params MySqlParameter[] paras)
        {
            try {
            conn.Open();//打开数据库
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            DataSet dt = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            cmd.Parameters.AddRange(paras);
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            conn.Close();//关闭数据库
            return dt.Tables[0];
            }
            catch(Exception ex)
            {
                throw new ApplicationException("数据查询出现错误"+ ex.Message);
            }

        }
        //静态数据增加函数
        public static bool DataAdd(string sql, params MySqlParameter[] paras)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(paras);
                var row = cmd.ExecuteNonQuery();
                conn.Close();
                if (row > 0)//增加成功
                {       
                    return true;
                }   
                return false;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("数据出入异常" + ex.Message);
            }
        }
        //静态数据更新函数
        public static bool DataUpdate(string sql,params MySqlParameter[]paras)
        {
            try { 
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            //添加多个参数
            cmd.Parameters.AddRange(paras);
            var row = cmd.ExecuteNonQuery();
            conn.Close();
            if (row > 0)
            {
                return true;
            }
            return false;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("数据更新异常" + ex.Message);
            }

        }
        //静态数据删除函数
        public static bool DataDelete(string sql ,params MySqlParameter[] paras)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(paras);
                var row = cmd.ExecuteNonQuery();
                conn.Close();
                if (row > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("数据删除异常" + ex.Message);
            }
        }

    }
}
