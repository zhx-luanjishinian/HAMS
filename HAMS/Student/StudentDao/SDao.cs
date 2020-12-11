using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using HAMS.DataUtil;
using System.Collections;
using System.Collections.Generic;

namespace HAMS.Student.StudentDao
{
    class SDao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable Login(String account, String pw)
        {
            
            String sql = "select stuSpecId,name,password from student where stuSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id",account);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;
           
        }
        public Dictionary<int,List<String>> showCourseInfo(String account)
        {
            //此类用来装返回的对象
            
            Dictionary<int,List<String>> dictionaryEntry = new Dictionary<int, System.Collections.Generic.List<String>>();
            String sql = "select stuId from student where stuSpecId=@id";
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataOperation.DataQuery(sql, parameter);
            String sql1 = "select classId from takecourse where stuId =@sid";
            MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[0][0].ToString());
            DataTable table1 = DataOperation.DataQuery(sql1, parameter1);
            String sql2 = "select teacherId,classSpecId,className from class where classId=@cid";
            String sql3 = "select name,department from teacher where teacherId=@tid";
            for(int i =0;i<table1.Rows.Count; i++)
            {
                //所有的信息都装在列表里面
                List<String> arrayList = new List<string>();
                //取每一条记录中的classid进行查询
                MySqlParameter para1 = new MySqlParameter("@cid", table1.Rows[i][0].ToString());
                DataTable table2 = DataOperation.DataQuery(sql2, para1);
                MySqlParameter para2 = new MySqlParameter("@tid", table2.Rows[0][0].ToString());
                DataTable table3 = DataOperation.DataQuery(sql3, para2);
                //添加老师的名字
                arrayList.Add(table3.Rows[0][0].ToString());
                //添加老师的学院
                arrayList.Add(table3.Rows[0][1].ToString());
                //添加课堂号
                arrayList.Add(table2.Rows[0][1].ToString());
                //添加课堂名字
                arrayList.Add(table2.Rows[0][2].ToString());
                dictionaryEntry.Add(i, arrayList);
            }
            return dictionaryEntry;
      

        }
        //查询作业公告的信息
        public List<String> showHomeNoticeInfo(String account)
        {
            List<String> result = new List<String>();
            //先查学生id(自增)
            String sql = "select stuId from student where stuSpecId=@id";
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataOperation.DataQuery(sql, parameter);
            //再到选课表里面查询课程id
            String sql1 = "select classId from takecourse where stuId =@sid";
            MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[0][0].ToString());
            DataTable table1 = DataOperation.DataQuery(sql1, parameter1);
            //查询作业的标题
            String sql2 = "select notTitle from notice where classId=@cid";
            for(int i = 0; i < table1.Rows.Count; i++)
            {
                MySqlParameter para = new MySqlParameter("@cid", table1.Rows[i][0].ToString());
                DataTable table2 = DataOperation.DataQuery(sql2, para);
                //一门课程不只有一个作业
                for(int j = 0; j < table2.Rows.Count; j++)
                {
                    result.Add(table2.Rows[j][0].ToString());
                }
            }
            return result;

        }
    }
}
