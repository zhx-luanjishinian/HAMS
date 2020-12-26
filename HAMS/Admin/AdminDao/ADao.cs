using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using HAMS.Entity;

namespace HAMS.Admin.AdminDao
{
    class ADao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable login(String account, String pw)
        {

            String sql = "select adminId,name,password from admin where adminId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);
            return table;
        }

        //按姓名查询教师信息
        public DataTable nameShowTeacher(string teacherName)
        {
            String sql = "select * from teacher where name = @name;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@name", teacherName);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //按工号查询教师信息
        public DataTable numberShowTeacher(string teacherSpecId)
        {
            String sql = "select * from teacher where teacherSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", teacherSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //按工号和姓名查询教师信息
        public DataTable showTeacher(string teaSpecId, string teaName)
        {
            String sql = "select * from teacher where teacherSpecId = @id and name = @name;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@id", teaSpecId);
            MySqlParameter para2 = new MySqlParameter("@name", teaName);

            DataTable table = DataUtil.DataOperation.dataQuery(sql, para1, para2);

            return table;
        }
        //根据自增id查看教师工号
        public DataTable showTeacherSpecId(int teaId)
        {
            String sql = "select teacherSpecId from teacher where teacherId = @id;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@id", teaId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para1);

            return table;
        }
        //根据教师工号查看自增id
        public DataTable showTeacherId(string teaId)
        {
            String sql = "select teacherId from teacher where teacherSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@id", teaId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para1);

            return table;
        }
        //修改教师信息
        public bool updateTeacherInfo(string teacherSpecId, string teacherName, int teacherSex, string teacherDep, string teacherPass)
        {
            String sql = "update teacher set name = @name,sex = @sex,department = @dep, password = @pass where teacherSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@name", teacherName);
            MySqlParameter para2 = new MySqlParameter("@sex", teacherSex);
            MySqlParameter para3 = new MySqlParameter("@dep", teacherDep);
            MySqlParameter para4 = new MySqlParameter("@pass", teacherPass);
            MySqlParameter para5 = new MySqlParameter("@id", teacherSpecId);
            return DataUtil.DataOperation.dataUpdate(sql, para1, para2, para3, para4, para5);//如果更新成功，则返回true
        }
        //按学号查询学生信息
        public DataTable numberStudentInfo(string stuSpecId)
        {
            String sql = "select * from student where stuSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", stuSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }

        //按姓名查询学生信息
        public DataTable nameShowStudent(string studentName)
        {
            String sql = "select * from student where name = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", studentName);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //按学号和姓名查询学生信息
        public DataTable showStudent(string stuSpecId,string studentName)
        {
            String sql = "select * from student where stuSpecId = @id and name = @name;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@id", stuSpecId);
            MySqlParameter para2 = new MySqlParameter("@name", studentName);

            DataTable table = DataUtil.DataOperation.dataQuery(sql, para1, para2);
            
            return table;
        }
        //修改学生信息
        public bool updateStudentInfo(string studentSpecId, string studentName, int studentSex, string studentClass, string studentPass)
        {
            String sql = "update student set name = @name,sex = @sex,classroom = @class, password = @pass where stuSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@name", studentName);
            MySqlParameter para2 = new MySqlParameter("@sex", studentSex);
            MySqlParameter para3 = new MySqlParameter("@class", studentClass);
            MySqlParameter para4 = new MySqlParameter("@pass", studentPass);
            MySqlParameter para5 = new MySqlParameter("@id", studentSpecId);
            return DataUtil.DataOperation.dataUpdate(sql, para1, para2, para3, para4, para5);//如果更新成功，则返回true
        }
        //根据课堂号查询课堂信息
        public DataTable numberShowClass(string classSpecId)
        {
            String sql = "select * from class where classSpecId = @number;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@number", classSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //根据课堂名查询课堂信息
        public DataTable nameShowClass(string className)
        {
            String sql = "select * from class where className = @name;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@name", className);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        
        //修改课堂信息
        public bool updateClassInfo(string classSpecId, string className, int teacherId)
        {
            String sql = "update class set className = @name,teacherId = @tid where classSpecId = @cid;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@name", className);
            MySqlParameter para2 = new MySqlParameter("@cid", classSpecId);
            MySqlParameter para3 = new MySqlParameter("@tid", teacherId);
            return DataUtil.DataOperation.dataUpdate(sql, para1, para3, para2);//如果更新成功，则返回true
        }

        //按课堂号和课堂名查询课堂信息
        public DataTable showClass(string classSpecId, string className)
        {
            String sql = "select * from class where classSpecId = @id and className = @name;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@id", classSpecId);
            MySqlParameter para2 = new MySqlParameter("@name", className);

            DataTable table = DataUtil.DataOperation.dataQuery(sql, para1, para2);

            return table;
        }
        //从数据库获取所有系统通知
        public DataTable getSysNotice(string adminId)
        {
            String sql = "select * from sysNotice where adminId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", adminId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //查询最新一条数据
        public DataTable newSysNotice()
        {
            String sql = "select * from (select * from sysNotice order by sysNotice.modifyTime desc) sysNotice";
          //String sql = "select * from sysNotice where sysId = (SELECT max(sysId) FROM sysNotice);";
            //传入要填写的参数

            DataTable table = DataUtil.DataOperation.dataQuery(sql);

            return table;
        }
        //插入系统通知
        public bool insertSysNotice(string SysTitle,string SysContent,string AdminId)
        {
            String sql = "insert into sysNotice (sysTitle,sysContent,adminId) values (@Systil,@Syscont,@AdminId);";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@Systil", SysTitle);
            MySqlParameter para2 = new MySqlParameter("@Syscont", SysContent);
            MySqlParameter para3 = new MySqlParameter("@AdminId", AdminId);
            return DataUtil.DataOperation.dataAdd(sql, para1, para2, para3);//如果插入成功，则返回true
        }
        //根据编号删除系统通知
        public Boolean deleteSysNotice(int sysNoticeId)
        {
            String sql = "delete from sysNotice where sysId=@sysId;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@sysId", sysNoticeId);
            return DataUtil.DataOperation.dataDelete(sql, para);//如果删除成功，则返回true
        }
        //修改系统通知
        public Boolean updateSysNotice(int sysNoticeId, string sysNotcieTitle, string sysNotciecontent)
        {
            String sql = "update sysNotice set sysContent = @sysNotciecontent,sysTitle = @sysNotcieTitle where sysId  = @sysNoticeId;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@sysNotciecontent", sysNotciecontent);
            MySqlParameter para2 = new MySqlParameter("@sysNotcieTitle", sysNotcieTitle);
            MySqlParameter para3 = new MySqlParameter("@sysNoticeId", sysNoticeId);
            return DataUtil.DataOperation.dataUpdate(sql, para1, para2, para3);//如果更新成功，则返回true
        }
    }
}
