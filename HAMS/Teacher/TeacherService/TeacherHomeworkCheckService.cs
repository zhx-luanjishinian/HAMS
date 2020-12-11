using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAMS.Teacher.TeacherDao;
using System.Data;
using System.Windows;

namespace HAMS.Teacher.TeacherService
{
    class TeacherHomeworkCheckService
    {
        private TDao td = new TDao();
        public String getHomURLByHomId(int homId)
        {
            //根据homId获取文件在服务器上的路径
            DataTable tbHomURL = td.getHomURLByHomId(homId);
            string homURL = tbHomURL.Rows[0][0].ToString();
            
            return homURL;
        }
        public String getPostilByHomId(int homId)
        {
            //根据homId获取学生的作业备注
            DataTable tbHomURL = td.getPostilByHomId(homId);
            string homURL = tbHomURL.Rows[0][0].ToString();
            return homURL;
        }
        public bool correctHomework(int homId, string score, string remark)
        {
            //批改作业，往数据库中写入成绩和点评
            bool flag = td.updateHomeworkByCorrect(homId,score,remark);
            return flag;
        }
    }
}
