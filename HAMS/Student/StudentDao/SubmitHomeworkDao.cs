using HAMS.Entity;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Student.StudentDao
{
    class SubmitHomeworkDao
    {
        public Boolean InsertHomework(Homework  homework)
        {
            String sql = "insert into homework (submitTime,postil,homURL,homName,stuId,teaId,classId) values (@subTime,@postil,@homUrl,@homName,@stuid,@teaid,@cid);";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@subTime", homework.SubmitTime);
            MySqlParameter para2 = new MySqlParameter("@postil", homework.Postil);
            MySqlParameter para3 = new MySqlParameter("@homUrl", homework.HomeURL);
            MySqlParameter para4 = new MySqlParameter("@homName", homework.HomeName);
            MySqlParameter para5 = new MySqlParameter("@stuid", homework.StuId);
            MySqlParameter para6 = new MySqlParameter("@teaid", homework.TeacherId);
            MySqlParameter para7 = new MySqlParameter("@cid", homework.ClassId);
            return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4, para5, para6 ,para7);  //插入成功时返回true
        }

        
    }
}
