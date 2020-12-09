using HAMS.Teacher.TeacherView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;



namespace HAMS.Teacher.TeacherDao
{
    class TDao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable Login(String account, String pw)
        {

            String sql = "select teacherSpecId,name,password from teacher where teacherSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            //MessageBox.Show(table.Rows[0][0].ToString());->stuSpecId
            //MessageBox.Show(table.Rows[0][1].ToString());->name
            //MessageBox.Show(table.Rows[0][2].ToString());->password
            return table;
        }
        public DataTable LoadMainFormLeft(string teacherSpecId)
        {
            //sql语句
            String sql = "select * from class where teacherId=@id";   //查询对应老师id的class表中所有的数据，这里查不到
            String sql1 = "select teacherId from teacher where teacherSpecId=@spaceId";  //根据当前老师的spaceId查询teacherId
            MySqlParameter parameter1 = new MySqlParameter("@spaceId", teacherSpecId);   //tbTeacherInfo.Text,这里我修改了一下，传过来的只有老师工号
            DataTable table1 = DataUtil.DataOperation.DataQuery(sql1, parameter1);
            //MessageBox.Show(table1.Rows[0][0].ToString());
            //以上是正确的
            MySqlParameter parameter = new MySqlParameter("@id", table1.Rows[0][0]);  //查到当前老师的teacherID
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);       //在class表中查老师教的课程名
        
            return table;
        }
        public DataTable getNotice(string classSpaceId)  //从数据库查询目前已有的作业
        {
            AnnounceNoticeDao temp = new AnnounceNoticeDao(); //为了使用查询classId的函数
            DataTable table0 = temp.getClassId(classSpaceId);  //查询需要的classId
            String sql = "select * from notice where classId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", table0.Rows[0][0]);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }

    }


    }

