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
            //MessageBox.Show(table.Rows[1][1].ToString());   //可以查到
            //生成可以动态增加的自定义控件组
            //TeachClass[] arrayTeachClass = new TeachClass[10];
            //给自定义控件的子控件加属性
            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    arrayTeachClass[i] = new TeachClass();
            //    arrayTeachClass[i].labelClassId1.Content = table.Rows[i][5];
            //    arrayTeachClass[i].labelClassName1.Content = table.Rows[i][1].ToString();
            //    listViewTeacherClass.Items.Add(arrayTeachClass[i]);
            //}
            return table;
        }
    }


    }
}
