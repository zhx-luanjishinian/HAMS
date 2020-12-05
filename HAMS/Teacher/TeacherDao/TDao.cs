using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace HAMS.Teacher.TeacherDao
{
    class TDao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable Login(String account, String pw)
        {

            String sql = "select teacherSpecId,name from teacher where teacherSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;


        }
    }
}
