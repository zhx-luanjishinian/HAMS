using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace HAMS.Admin.AdminDao
{
    class ADao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable Login(String account, String pw)
        {

            String sql = "select adminId,name,password from admin where adminId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;
        }
        public DataTable getAdminId(string classSpecId)
        {
            MySqlParameter para = new MySqlParameter("@id", classSpecId);
            //根据真实的课堂号获取课堂表里的自增主键课堂号classId
            String sql = "select adminId from admin where classSpecId = @id;";
            //传入要填写的参数
            
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //从数据库获取所有系统通知
        public DataTable getSysNotice(string adminId)  
        {
            String sql = "select * from sysNotice where adminId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", adminId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //按姓名查询教师信息
        public DataTable nameShowTeacher(string teacherName)
        {
            String sql = "select * from teacher where name = @name;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@name", teacherName);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //按工号查询教师信息
        public DataTable numberShowTeacher(string teacherId)
        {
            String sql = "select * from teacher where teacherId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", teacherId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //按学号查询学生信息
        public DataTable numberShowStudent(string studentId)
        {
            String sql = "select * from sysNotice where stuId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", studentId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //按姓名查询学生信息
        public DataTable nameShowStudent(string studentName)
        {
            String sql = "select * from student where name = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", studentName);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //根据课堂号查询课堂信息
        public DataTable numberShowClass(string classNumber)
        {
            String sql = "select * from class where classSpecId = @number;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@number", classNumber);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //根据课堂名查询课堂信息
        public DataTable nameShowClass(string className)
        {
            String sql = "select * from class where className = @name;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@name", className);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
    }
}
